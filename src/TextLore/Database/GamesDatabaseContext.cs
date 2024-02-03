using Microsoft.EntityFrameworkCore;

namespace TextLore.Database;

public class GamesDatabaseContext(DbContextOptions<GamesDatabaseContext> options) : DbContext(options)
{
    public DbSet<Games.Roguelike.Models.RoomDefinition> RoguelikeRooms { get; set; }
}