
using GarageApp.Interfaces;
using GarageApp.Models;

namespace GarageApp;

internal class GarageManager
{

    private readonly IUI _ui;
    private readonly IGarageHandler<IVehicle> _garageHandler;

    private IEnumerable<MenuItem> _menu = new List<MenuItem>();

    public GarageManager(IUI ui, IGarageHandler<IVehicle> handler)
    {
        _ui = ui;
        _garageHandler = handler;
        _menu = new List<MenuItem>()
        {
            new MenuItem() {Key = ConsoleKey.A, Label = "Lista parkerade fordon", Action = ListAllVehicles},
            new MenuItem() {Key = ConsoleKey.B, Label = "Skapa och parkera ett fordon", Action = CreateAndAddVehicle},
            new MenuItem() {Key = ConsoleKey.C, Label = "Skapa och parkera ett gäng fordon", Action = BatchCreateAndAddVehicles},
        };
    }

    private void BatchCreateAndAddVehicles()
    {
        _ui.PrintLine("Dessa fordon har parkerat:");
        IVehicle[] vehicles = new IVehicle[] {
            new Car() {Manufacturer = "BMW", Model = "Z3", WheelCount = 4, RegistrationNumber = "AKU588"},
            new Bike() {Manufacturer = "Crescent", Model = "7x", WheelCount = 2, RegistrationNumber = "K7748397264"},
            new Car() {Manufacturer = "Saab", Model = "9000", WheelCount = 4, RegistrationNumber = "HRW668"},
            new Car() {Manufacturer = "Audi", Model = "B3", WheelCount = 4, RegistrationNumber = "HI798P"},
        };
        foreach (var vehicle in vehicles)
        {
            _garageHandler.AddVehicle(vehicle);
            _ui.PrintLine(vehicle.ToString());
        }
    }

    private void CreateAndAddVehicle()
    {
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

    private void ListAllVehicles()
    {
        var vehicles = _garageHandler.Vehicles;
        if (vehicles.Count() == 0)
        {
            _ui.PrintLine("Garaget är tomt");
            return;
        }
        foreach (var vehicle in vehicles)
        {
            _ui.PrintLine(vehicle.ToString());
        }
    }

    internal void Run()
    {
        PrintMenu();
    }

    private void PrintMenu()
    {
        MenuItem exitItem = new MenuItem() { Key = ConsoleKey.Q, Label = "Avsluta" };
        _ui.PrintMenu(_menu, exitItem, "Vad vill du göra?");
    }

    private void Test()
    {
        _ui.PrintLine("Yep...");
    }
}
