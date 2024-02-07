using TextLore.Shared.Models.Level;

namespace TextLore.Components.Pages.Games.Roguelike.Models;

public class Enemy(string key, string name, string description, int maxHealth, Weapon? weapon) : IRoomObject
{
    public string Key => key;

    public string Name => name;

    public string Description => description;

    public int MaxHealth { get; } = maxHealth;

    public int Health { get; set; } = maxHealth;

    public Weapon? Weapon { get; set; } = weapon;
}