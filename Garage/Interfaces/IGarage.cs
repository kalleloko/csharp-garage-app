namespace GarageApp.Interfaces;

internal interface IGarage<T> : IEnumerable<T> where T : IVehicle
{
    public T[] Vehicles { get; set; }
    public int MaxCapacity { set; get; }
}