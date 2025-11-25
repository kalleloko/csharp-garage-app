namespace GarageApp.Interfaces;

public class MenuItem : IMenuItem
{
    public ConsoleKey Key { get; init; }
    public string Label { get; init; } = string.Empty;
    public Action? Action { get; init; }
    public IEnumerable<IMenuItem>? SubItems { get; init; }

}