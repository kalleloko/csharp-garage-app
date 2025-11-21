using GarageApp.Interfaces;

namespace GarageApp;

internal class Program
{

    static void Main(string[] args)
    {
        IUI ui = new ConsoleUI();
        IGarage garage = new Garage();
        IGarageHandler garageHandler = new GarageHandler(garage);
        GarageManager.Init(ui, garageHandler);
    }
}
