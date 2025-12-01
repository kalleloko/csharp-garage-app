namespace GarageApp.Models;

internal class Bike : Vehicle
{
    public required int GearCount { get; set; }
    public Bike()
    {
        _wheelCount = 2;
    }

    public override string ToString()
    {
        return base.ToString() + $", {GearCount}-växlad";
    }
}