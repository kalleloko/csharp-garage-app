namespace GarageApp.Interfaces;

public interface IMenuItem
{
    ConsoleKey Key { get; init; }
    string Label { get; init; }
    Action? Action { get; init; }
    IEnumerable<IMenuItem>? SubItems { get; init; }
}