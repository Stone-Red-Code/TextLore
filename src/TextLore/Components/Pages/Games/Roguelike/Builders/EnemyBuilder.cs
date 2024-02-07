using TextLore.Components.Pages.Games.Roguelike.Models;
using TextLore.Shared.Logic;

namespace TextLore.Components.Pages.Games.Roguelike.Builders;

public class EnemyBuilder : IBuilder<Enemy>
{
    private string name = string.Empty;
    private string description = string.Empty;
    private int maxHealth = 0;

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

    public Enemy Build()
    {
        return new Enemy(name, description, maxHealth);
    }
}