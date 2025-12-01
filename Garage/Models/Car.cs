using GarageApp.Enums;

namespace GarageApp.Models;

internal class Car : Vehicle
{
    private Fueltype _fuelType;

    public Fueltype FuelType
    {
        get { return _fuelType; }
        set { _fuelType = value; }
    }

}
