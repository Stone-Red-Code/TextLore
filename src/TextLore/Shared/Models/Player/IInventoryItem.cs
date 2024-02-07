namespace TextLore.Shared.Models.Player;

public interface IInventoryItem
{
    public string Name { get; }

    public string Description { get; }

    public string Key { get; }
}