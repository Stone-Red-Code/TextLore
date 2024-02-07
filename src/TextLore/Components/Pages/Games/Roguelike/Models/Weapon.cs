using TextLore.Shared.Models.Player;

namespace TextLore.Components.Pages.Games.Roguelike.Models;

public class Weapon(string key, string name, string description) : IInventoryItem
{
    public string Key { get; } = key;

    public string Name { get; } = name;
    public string Description { get; } = description;

    public void Shoot()
    {
        // Shoot the weapon
    }
}