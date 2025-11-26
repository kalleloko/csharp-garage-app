using GarageApp.Interfaces;

namespace GarageApp;

internal class VehicleBuilder : IVehicleBuilder
{
    private readonly IUI _ui;
    public VehicleBuilder(IUI ui)
    {
        _ui = ui;
    }
    public IVehicle CreateVehicle()
    {
        throw new NotImplementedException();
    }
}