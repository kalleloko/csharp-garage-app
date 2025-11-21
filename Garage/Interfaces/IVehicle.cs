namespace GarageApp.Interfaces;

internal interface IVehicle
{
    public string Model { get; init; }
    public string Manufacturer { get; init; }
    public int WheelCount { get; init; }
    public string RegistrationNumber { get; init; }

}
