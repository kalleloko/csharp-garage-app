using GarageApp.Interfaces;
using System.Collections;

namespace GarageApp.Models;

public class Garage<T> : IGarage<T> where T : IVehicle
{
    private int _capacity = 1;
    private static int _maxCapacity = 10000;
    public T[] Vehicles { get; private set; }
    public int Capacity
    {
        get => _capacity;
        init
        {
            if (value < 1 || value > _maxCapacity)
            {
                throw new ArgumentOutOfRangeException($"Garagets kapacitet måste vara mellan 1 och {_maxCapacity}");
            }
            _capacity = value;
        }
    }

    public Garage() : this(100)
    {
    }

    public Garage(int capacity)
    {
        Capacity = capacity;
        Vehicles = new T[capacity];
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (T vehicle in Vehicles)
        {
            if (vehicle is null)
            {
                continue;
            }
            yield return vehicle;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}