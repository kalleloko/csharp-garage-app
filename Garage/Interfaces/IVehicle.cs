using GarageApp.Enums;

namespace GarageApp.Interfaces;

public interface IVehicle
{
    public string Model { get; init; }
    public Manufacturer Manufacturer { get; init; }
    public int WheelCount { get; init; }
    public string RegistrationNumber { get; init; }

}
