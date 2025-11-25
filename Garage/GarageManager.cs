
using GarageApp.Interfaces;

namespace GarageApp;

internal class GarageManager
{

    private static IUI _ui;
    private static IGarageHandler<IVehicle> _handler;

    private static IEnumerable<MenuItem> _menu = new List<MenuItem>()
    {

    };
    internal static void Init(IUI ui, IGarageHandler<IVehicle> garageHandler)
    {
        _ui = ui;
        _handler = garageHandler;
        PrintMenu();
    }

    private static void PrintMenu()
    {
        MenuItem exitItem = new MenuItem() { Key = ConsoleKey.Q, Label = "Tillbaka" };
        _ui.PrintMenu(_menu, exitItem, "Vad vill du göra?");
    }

    private static void Test()
    {
        _ui.PrintLine("Yep...");
    }
}
