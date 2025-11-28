
using GarageApp.Interfaces;
using GarageApp.Models;
using System.Text;

namespace GarageApp;

internal class GarageManager
{

    private readonly IUI _ui;
    private readonly IGarageHandler<IVehicle> _garageHandler;
    private readonly ISearchParser _searchParser;

    public GarageManager(IUI ui, IGarageHandler<IVehicle> handler, ISearchParser searchParser)
    {
        _ui = ui;
        _garageHandler = handler;
        _searchParser = searchParser;
    }

    internal void Run()
    {
        _ui.PrintLine("Det är här garage-appen. Till att börja med måste du skapa ett garage.");
        CreateGarage();
        _ui.PrintEmptyLines();

        MenuItem exitItem = new MenuItem() { Key = ConsoleKey.Q, Label = "Avsluta" };
        List<MenuItem> menu = new List<MenuItem>()
            {
                new MenuItem() {Key = ConsoleKey.A, Label = "Lista parkerade fordon", Action = PrintAllVehicles},
                new MenuItem() {Key = ConsoleKey.B, Label = "Lista parkerade fordon (grupperat efter typ)", Action = PrintAllVehiclesByType},
                new MenuItem() {Key = ConsoleKey.C, Label = "Skapa och parkera ett fordon", Action = CreateAndAddVehicle},
                new MenuItem() {Key = ConsoleKey.D, Label = "Kör ut fordon från garaget", Action = RemoveVehicle},
                new MenuItem() {Key = ConsoleKey.E, Label = "Sök fordon", Action = SearchVehicle},
                new MenuItem() {Key = ConsoleKey.F, Label = "Auto-skapa och parkera ett gäng fordon", Action = BatchCreateAndAddVehicles},
            };
        _ui.PrintMenu(menu, exitItem, "Vad vill du göra?");
    }

    private void SearchVehicle()
    {
        _ui.PrintLine("Sök med dessa flaggor:");
        string search = _ui.AskForInput<string>(_searchParser.Instructions) ?? string.Empty;
        IEnumerable<IVehicle> vehicles = _garageHandler.Vehicles;
        foreach (Func<IVehicle, bool> filterIsApplied in _searchParser.Parse(search))
        {
            vehicles = vehicles.Where(filterIsApplied);
        }
    }

    private void CreateGarage()
    {
        int cap = _ui.AskForInput<int>("Ange hur många platser garaget ska ha:");
        try
        {
            _garageHandler.MaxCapacity = cap;
        }
        catch (Exception e)
        {
            _ui.PrintErrorLine(e.Message);
        }
    }

    private void RemoveVehicle()
    {
        if (_garageHandler.Vehicles.Count() <= 0)
        {
            _ui.PrintLine("Garaget är redan tomt");
            return;
        }
        List<IVehicle> orderedVehicles = _garageHandler.Vehicles.ToList();
        IVehicle? vehicle = null;
        while (vehicle is null)
        {

            PrintVehicles(orderedVehicles, "");
            int indexToRemove = _ui.AskForInput<int>("Välj vilket fordon som ska köra ut...(0 för att avbryta)") - 1;

            if (indexToRemove == -1)
            {
                return;
            }
            vehicle = orderedVehicles.ElementAtOrDefault(indexToRemove);

            if (vehicle is null)
            {
                _ui.PrintErrorLine("Ogiltigt fordonsnummer, försök igen!");
            }
        }
        string? doItWhenY = _ui.AskForInput<string>($"Vill du köra ut '{vehicle.ToString()}'? (Y/N)");
        if ((doItWhenY is not null) && (doItWhenY.ToUpper() == "Y"))
        {
            try
            {
                _garageHandler.RemoveVehicle(vehicle);
                _ui.PrintLine($"'{vehicle.ToString()}' körde ut!");
            }
            catch (Exception e)
            {
                _ui.PrintErrorLine(e.Message);
            }
        }
    }

    private void PrintAllVehiclesByType()
    {
        foreach (IGrouping<string, IVehicle> group in _garageHandler.VehiclesByType)
        {
            PrintVehicles(group, $"{group.Key}s: ({group.Count()})");
            _ui.PrintEmptyLines();
        }
    }

    private void BatchCreateAndAddVehicles()
    {

        IVehicle[] vehicles = new IVehicle[] {
            new Car() {Manufacturer = "BMW", Model = "Z3", WheelCount = 4, RegistrationNumber = "AKU588"},
            new Bike() {Manufacturer = "Crescent", Model = "7x", WheelCount = 2, RegistrationNumber = "K7748397264"},
            new Car() {Manufacturer = "Saab", Model = "9000", WheelCount = 4, RegistrationNumber = "HRW668"},
            new Car() {Manufacturer = "Audi", Model = "B3", WheelCount = 4, RegistrationNumber = "HI798P"},
        };
        StringBuilder sb = new StringBuilder();

        foreach (var vehicle in vehicles)
        {
            try
            {
                _garageHandler.AddVehicle(vehicle);
                sb.AppendLine(vehicle.ToString());
            }
            catch (InvalidOperationException e)
            {
                _ui.PrintErrorLine(e.Message);
                // garage is full, we can break out of here
                return;
            }
            catch (Exception e)
            {
                _ui.PrintErrorLine(e.Message);
            }
        }
        if (sb.Length > 0)
        {
            sb = new StringBuilder()
                .AppendLine("Dessa fordon har parkerat:")
                .Append(sb.ToString());
            _ui.PrintLine(sb.ToString());
        }
    }

    private void CreateAndAddVehicle()
    {
        if (_garageHandler.GarageIsFull())
        {
            _ui.PrintErrorLine("Garaget är fullt");
        }
        IVehicle? vehicle = MakeVehicle();

        if (vehicle is null)
        {
            _ui.PrintErrorLine("Kunde inte skapa något fordon av din input!");
            return;
        }

        try
        {
            _garageHandler.AddVehicle(vehicle);
            _ui.PrintLine("Detta fordon har parkerat:");
            _ui.PrintLine(vehicle.ToString());
        }
        catch (Exception e)
        {
            _ui.PrintErrorLine(e.Message);
        }
    }

    private IVehicle? MakeVehicle()
    {
        string typeInput = "";
        List<MenuItem> menu = new List<MenuItem>()
        {
            new MenuItem() {Key = ConsoleKey.A, Label = "Bil", Action = () => typeInput = "car"},
            new MenuItem() {Key = ConsoleKey.B, Label = "Cykel", Action = () => typeInput = "bike"},
        };
        MenuItem exitItem = new MenuItem() { Key = ConsoleKey.Q, Label = "Tillbaka" };
        _ui.InvokeOneMenuAction(menu, exitItem, "Vilken typ av fordon vill du skapa?");

        //string? typeInput = _ui.AskForInput<string>("Vilken typ av fordon vill du skapa?");

        if (typeInput is null)
        {
            return null;
        }

        switch (typeInput.ToLower())
        {
            case "car":
                return new VehicleBuilder<Car>(_ui).Build();

            case "bike":
                return new VehicleBuilder<Bike>(_ui).Build();

            default:
                return null;
        }

    }

    private void PrintAllVehicles()
    {
        PrintVehicles(
            _garageHandler.Vehicles,
            $"Garaget har {_garageHandler.Vehicles.Count()} fordon på sina {_garageHandler.MaxCapacity} platser:",
            "Garaget är tomt"
        );
    }

    private void PrintVehicles(IEnumerable<IVehicle> vehicles, string heading, string? headingWhenEmpty = "")
    {
        if (vehicles.Count() == 0)
        {
            _ui.PrintLine(headingWhenEmpty);
            return;
        }
        _ui.PrintLine(heading + Environment.NewLine);
        int i = 1;
        foreach (var vehicle in vehicles)
        {
            _ui.PrintLine($"{i}. {vehicle.ToString()}");
            i++;
        }
    }
}
