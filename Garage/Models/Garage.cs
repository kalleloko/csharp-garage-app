using GarageApp.Interfaces;
using System.Collections;

namespace GarageApp.Models;

public class Garage<T> : IGarage<T> where T : IVehicle
{
    private int _maxCapacity = 0;
    private bool _isInitialized = false;
    private static int _globalMaxCapacity = 10000;

    private T?[] _vehicles = [];
    public int MaxCapacity
    {
        get => _maxCapacity;
        set
        {
            if (_isInitialized)
            {
                throw new InvalidOperationException("MaxCapacity kan bara sättas en gång.");
            }
            if (value < 1 || value > _globalMaxCapacity)
            {
                throw new ArgumentOutOfRangeException($"Garagets kapacitet måste vara mellan 1 och {_globalMaxCapacity}");
            }
            _maxCapacity = value;
            _isInitialized = true;
            _vehicles = new T[value];
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (T? vehicle in _vehicles)
        {
            if (vehicle is not null)
            {
                yield return vehicle;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public bool AddVehicle(T vehicle)
    {
        bool result = false;
        if (!IsFull() && (GetVehicle(vehicle) is null))
        {
            for (int i = 0; i < MaxCapacity; i++)
            {
                if (_vehicles[i] is null)
                {
                    _vehicles[i] = vehicle;
                    result = true;
                    break;
                }
            }
        }
        return result;
    }
    public bool RemoveVehicle(T vehicle)
    {
        bool result = false;
        for (int i = 0; i < _vehicles.Length; i++)
        {
            if (Equals(_vehicles[i], vehicle))
            {
                _vehicles[i] = default(T);
                result = true;
                break;
            }
        }
        return result;
    }

    public T? GetVehicle(T vehicle)
    {
        return _vehicles.Where(v => (v is not null) && (v.RegistrationNumber == vehicle.RegistrationNumber)).FirstOrDefault();
    }

    public int GetCapacity()
    {
        return MaxCapacity - _vehicles.Where(v => v != null).Count();
    }

    public bool IsFull()
    {
        return GetCapacity() == 0;
    }
}