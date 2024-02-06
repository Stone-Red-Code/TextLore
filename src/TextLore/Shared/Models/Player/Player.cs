namespace TextLore.Shared.Models.Player;

public class Player
{
    public int Health { get; set; } = 100;

    public List<InventoryItem> Inventory { get; } = [];

    public Position Position { get; set; } = new Position(0, 0);
}