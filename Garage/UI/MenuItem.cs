namespace GarageApp.Interfaces;

public class MenuItem : IMenuItem
{
    public ConsoleKey Key { get; init; }
    public string Label { get; init; } = string.Empty;
    public Action? Action { get; init; }
    public IEnumerable<IMenuItem>? SubItems { get; init; }
    public IMenuItem WithLabel(string newLabel)
    {
        return new MenuItem
        {
            Key = this.Key,
            Label = newLabel,
            Action = this.Action,
            SubItems = this.SubItems
        };
    }

}