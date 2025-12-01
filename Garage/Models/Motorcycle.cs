using GarageApp.Enums;

namespace GarageApp.Models;

internal class Motorcycle : Vehicle
{
    public required Fueltype FuelType
    {
        get;
        set;
    }
    public Motorcycle()
    {
        _wheelCount = 2;
    }

    public override string ToString()
    {
        return base.ToString() + $", Bränsletyp: {FuelType}";
    }
}