namespace GarageApp.Interfaces;

internal interface IGarage<T> : IEnumerable<T> where T : IVehicle
{
    public IEnumerable<T> Vehicles { get; }
    public int MaxCapacity { set; get; }
}