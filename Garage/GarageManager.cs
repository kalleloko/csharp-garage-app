
using GarageApp.Interfaces;
using GarageApp.Models;

namespace GarageApp;

internal class GarageManager
{

    private readonly IUI _ui;
    private readonly IGarageHandler<IVehicle> _handler;

    private IEnumerable<MenuItem> _menu = new List<MenuItem>();

    public GarageManager(IUI ui, IGarageHandler<IVehicle> handler)
    {
        _ui = ui;
        _handler = handler;
        _menu = new List<MenuItem>()
        {
            new MenuItem() {Key = ConsoleKey.A, Label = "Lista parkerade fordon", Action = ListAllVehicles},
            new MenuItem() {Key = ConsoleKey.B, Label = "Parkera fordon", Action = AddVehicle},
        };
    }

    private void AddVehicle()
    {
        IVehicle vehicle = MakeVehicle();

        if (vehicle is null)
        {
            _ui.PrintErrorLine("Kunde inte skapa något fordon av din input!");
            return;
        }

        try
        {
            _handler.AddVehicle(vehicle);
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
        string? typeInput = _ui.AskForInput<string>("Vilken typ av fordon vill du skapa?");

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
        var vehicles = _handler.Vehicles;
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
