using GarageApp.Interfaces;

namespace GarageApp.Models;

internal class GarageHandler<T> : IGarageHandler<T> where T : IVehicle
{
    private IGarage<T> _garage;

    public GarageHandler(IGarage<T> garage)
    {
        _garage = garage;
    }

    public IEnumerable<T> Vehicles
    {
        get => (IEnumerable<T>)_garage.Vehicles.Where(v => v is not null);
    }

    public IEnumerable<KeyValuePair<string, IEnumerable<T>>> VehiclesByType => throw new NotImplementedException();

    public bool AddVehicle(T vehicle)
    {
        throw new NotImplementedException();
    }

    public T? FindVehicle(string registrationNumber)
    {
        throw new NotImplementedException();
    }

    public bool RemoveVehicle(T vehicle)
    {
        throw new NotImplementedException();
    }

    public bool RemoveVehicle(string registrationNumber)
    {
        throw new NotImplementedException();
    }
}