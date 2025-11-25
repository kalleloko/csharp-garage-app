using GarageApp.Interfaces;

namespace GarageApp.Models;

internal class GarageHandler<T> : IGarageHandler<T> where T : IVehicle
{
    private IGarage<T> _garage;

    public GarageHandler(IGarage<T> garage)
    {
        _garage = garage;
    }
}