namespace GarageApp.Models;

internal class Airplane : Vehicle
{
    public required int CrewCount { get; set; }
    public required int PassengerSeatCount { get; set; }
    public override string ToString()
    {
        return $"{CrewCount}-sitsig " + base.ToString() + $", plats för {PassengerSeatCount} passagerare";
    }
}