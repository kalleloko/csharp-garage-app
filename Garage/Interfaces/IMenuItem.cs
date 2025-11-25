namespace GarageApp.Interfaces;

public interface IMenuItem
{
    string Label { get; }
    Action? Action { get; }
    IEnumerable<IMenuItem>? SubItems { get; }
}