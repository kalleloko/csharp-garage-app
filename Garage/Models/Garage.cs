using GarageApp.Interfaces;
using System.Collections;

namespace GarageApp.Models;

internal class Garage : IGarage
{
    public IEnumerator<Vehicle> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}