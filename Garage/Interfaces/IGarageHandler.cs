namespace GarageApp.Interfaces;

internal interface IGarageHandler<T> where T : IVehicle
{
    public IEnumerable<T> Vehicles { get; }
    public IEnumerable<IGrouping<string, T>> VehiclesByType { get; }
    public int MaxCapacity { get; }
    public void AddVehicle(T vehicle);
    public void RemoveVehicle(T vehicle);
    public void RemoveVehicle(string registrationNumber);
    public T? FindVehicle(T vehicle);
    public T? FindVehicle(string registrationNumber);
}