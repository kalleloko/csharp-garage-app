namespace GarageApp.Interfaces;

internal interface ISearchParser
{
    public string Instructions { get; }
    IEnumerable<Func<IVehicle, bool>> Parse(string search);
}