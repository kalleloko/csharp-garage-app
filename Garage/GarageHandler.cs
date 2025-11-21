using GarageApp.Interfaces;

namespace GarageApp;

internal class GarageHandler : IGarageHandler
{
    private IGarage _garage;

    public GarageHandler(IGarage garage)
    {
        _garage = garage;
    }
}