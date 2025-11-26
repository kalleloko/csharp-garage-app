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
        IVehicleBuilder vehicleFactory = new VehicleBuilder(ui);

        GarageManager manager = new(ui, garageHandler, vehicleFactory);

        manager.Run();
    }
}
