using Microsoft.EntityFrameworkCore;

using TextLore.Shared.Models.Level;

namespace TextLore.Database;

public class GamesDatabaseContext(DbContextOptions<GamesDatabaseContext> options) : DbContext(options)
{
    public DbSet<RoomDefinition> Rooms { get; set; }
}