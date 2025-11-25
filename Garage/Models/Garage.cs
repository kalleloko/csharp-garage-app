using GarageApp.Interfaces;
using System.Collections;

namespace GarageApp.Models;

public class Garage<T> : IGarage<T> where T : IVehicle
{
    public T[] Vehicles { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public int MaxCapacity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}