using TextLore.Shared.Models.Player;

namespace TextLore.Components.Pages.Games.Roguelike.Models;

public class Weapon(string name, string description, string tag) : IInventoryItem
{
    public string Name { get; } = name;
    public string Description { get; } = description;
    public string Tag { get; } = tag;

    public void Shoot()
    {
        // Shoot the weapon
    }
}