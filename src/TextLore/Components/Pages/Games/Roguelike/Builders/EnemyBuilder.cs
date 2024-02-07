using TextLore.Components.Pages.Games.Roguelike.Models;
using TextLore.Shared.Logic;

namespace TextLore.Components.Pages.Games.Roguelike.Builders;

public class EnemyBuilder : IBuilder<Enemy>
{
    private string key = string.Empty;
    private string name = string.Empty;
    private string description = string.Empty;
    private int maxHealth = 0;
    private Weapon? weapon = null;

    public EnemyBuilder WithKey(string key)
    {
        this.key = key;
        return this;
    }

    public EnemyBuilder WithName(string name)
    {
        this.name = name;
        return this;
    }

    public EnemyBuilder WithDescription(string description)
    {
        this.description = description;
        return this;
    }

    public EnemyBuilder WithMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
        return this;
    }

    public EnemyBuilder WithWeapon(Weapon weapon)
    {
        this.weapon = weapon;
        return this;
    }

    public Enemy Build()
    {
        return new Enemy(key, name, description, maxHealth, weapon);
    }
}