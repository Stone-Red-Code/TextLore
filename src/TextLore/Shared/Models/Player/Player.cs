namespace TextLore.Shared.Models.Player;

public class Player
{
    public int Health { get; set; } = 100;

    public List<IInventoryItem> Inventory { get; } = [];

    public Position PositionInLevel { get; set; } = new Position(0, 0);

    public Position PositionInRoom { get; set; } = new Position(0, 0);
}