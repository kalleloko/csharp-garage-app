namespace GarageApp.Models;

internal abstract class Vehicle
{
    private int _wheelCount = 4;

    public int WheelCount
    {
        get => _wheelCount;
        private set { }
    }

}