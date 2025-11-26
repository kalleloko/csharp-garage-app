
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
        Car c = new() { Manufacturer = "BMW", Model = "Wha", RegistrationNumber = "Rgd567", WheelCount = 4 };
        try
        {
            _handler.AddVehicle(c);
            _ui.PrintLine("Detta fordon har parkerat:");
            _ui.PrintLine(c.ToString());
        }
        catch (Exception e)
        {
            _ui.PrintErrorLine(e.Message);
        }
        //IEnumerable<MenuItem> menu = new List<MenuItem>()
        //{
        //    new MenuItem() {Key = ConsoleKey.A, Label = "Hej", Action = ListAllVehicles},
        //    new MenuItem() {Key = ConsoleKey.B, Label = "", Action = AddVehicle},
        //};
        //MenuItem exitItem = new MenuItem() { Key = ConsoleKey.Q, Label = "Tillbaka" };
        //_ui.PrintMenu(menu, exitItem, "Vad vill du göra?");
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
