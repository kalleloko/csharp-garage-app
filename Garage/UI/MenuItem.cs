namespace GarageApp.Interfaces;

public class MenuItem : IMenuItem
{
    public string Label { get; } = string.Empty;
    public Action? Action { get; }
    public IEnumerable<IMenuItem>? SubItems { get; }
}