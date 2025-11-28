namespace GarageApp.Interfaces;

internal interface ISearchParser
{
    string Instructions { get; }
    Func<IVehicle, bool> Parse(string search);
}