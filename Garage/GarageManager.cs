
using GarageApp.Interfaces;

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
            new MenuItem() {Key = ConsoleKey.A, Label = "Lista parkerade fordon", Action = ListAllVehicles}
        };
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
        MenuItem exitItem = new MenuItem() { Key = ConsoleKey.Q, Label = "Tillbaka" };
        _ui.PrintMenu(_menu, exitItem, "Vad vill du göra?");
    }

    private void Test()
    {
        _ui.PrintLine("Yep...");
    }
}
