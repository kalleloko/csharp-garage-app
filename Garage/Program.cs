using GarageApp.Interfaces;
using GarageApp.Models;
using GarageApp.UI;

namespace GarageApp;

internal class Program
{

    static void Main(string[] args)
    {
        IUI ui = new ConsoleUI();
        IGarage<IVehicle> garage = new Garage<IVehicle>();
        IGarageHandler<IVehicle> garageHandler = new GarageHandler<IVehicle>(garage);

        GarageManager.Init(ui, garageHandler);
    }
}
