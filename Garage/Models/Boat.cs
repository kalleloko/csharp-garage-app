namespace GarageApp.Models;

internal class Boat : Vehicle
{
    public required int MastCount { get; set; } = 2;
    public override string ToString()
    {
        return base.ToString() + $", med {MastCount} master";
    }
}