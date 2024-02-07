using TextLore.Shared.Models.Level;

namespace TextLore.Components.Pages.Games.Roguelike.Models;

public class Enemy(string name, string description, int maxHealth) : IRoomObject
{
    public string Name => name;

    public string Description => description;

    public int MaxHealth { get; } = maxHealth;

    public int Health { get; set; } = maxHealth;
}