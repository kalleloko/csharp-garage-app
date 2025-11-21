namespace GarageApp.Interfaces;

internal interface IGarageHandler<T> where T : IVehicle
{
    public IEnumerable<T> Vehicles { get; }
    public IEnumerable<KeyValuePair<string, IEnumerable<T>>> VehiclesByType { get; }
    public bool AddVehicle(T vehicle);

    public bool RemoveVehicle(T vehicle);
    public bool RemoveVehicle(string registrationNumber);
    public T? FindVehicle(string registrationNumber);
    //TODO: ska LINQ-metoder verkligen finnas i interfacet?
    public T? FindVehicle();
}