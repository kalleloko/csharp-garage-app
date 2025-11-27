namespace GarageApp.Interfaces;

internal interface IGarage<T> : IEnumerable<T> where T : IVehicle
{
    public int MaxCapacity { get; set; }
    public bool AddVehicle(T vehicle);
    public bool RemoveVehicle(T vehicle);
    public T? GetVehicle(T vehicle);
    public int GetCapacity();
    public bool IsFull();

}