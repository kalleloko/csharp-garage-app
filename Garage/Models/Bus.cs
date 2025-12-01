namespace GarageApp.Models;

internal class Bus : Vehicle
{
    public required int SeatCount { get; set; }

    public override string ToString()
    {
        return base.ToString() + $", plats för {SeatCount} passagerare";
    }
}