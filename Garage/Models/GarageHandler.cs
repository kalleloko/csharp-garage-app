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
        get => _garage;
    }

    public IEnumerable<IGrouping<string, T>> VehiclesByType => Vehicles.GroupBy(v => v.GetType().Name);

    public void AddVehicle(T vehicle)
    {
        if (_garage.IsFull())
        {
            throw new InvalidOperationException("Garaget är fullt.");
        }
        if (_garage.GetVehicle(vehicle) is not null)
        {
            throw new ArgumentException($"Fordonet '{vehicle.RegistrationNumber}' finns redan i garaget");
        }
        _garage.AddVehicle(vehicle);
    }

    public T? FindVehicle(string registrationNumber)
    {
        return _garage.Where(v => v.RegistrationNumber == registrationNumber).FirstOrDefault();
    }

    public T? FindVehicle(T vehicle)
    {
        return _garage.GetVehicle(vehicle);
    }

    public void RemoveVehicle(T vehicle)
    {
        bool success = _garage.RemoveVehicle(vehicle);
        if (!success)
        {
            throw new ArgumentException($"Fordonet '{vehicle.RegistrationNumber}' fanns inte i garaget");
        }
    }

    public void RemoveVehicle(string registrationNumber)
    {
        T? vehicle = FindVehicle(registrationNumber);
        if (vehicle is null)
        {
            throw new ArgumentException($"Fordonet '{registrationNumber}' fanns inte i garaget");
        }
        RemoveVehicle(vehicle);
    }
}