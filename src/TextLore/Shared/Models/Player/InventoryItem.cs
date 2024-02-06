namespace TextLore.Shared.Models.Player;

public class InventoryItem(string name, string description, string tag)
{
    public string Name { get; set; } = name;

    public string Description { get; set; } = description;

    public string Tag { get; set; } = tag;
}