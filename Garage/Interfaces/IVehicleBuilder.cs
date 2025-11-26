namespace GarageApp.Interfaces
{
    internal interface IVehicleBuilder<T> where T : IVehicle
    {
        T Build();
    }
}