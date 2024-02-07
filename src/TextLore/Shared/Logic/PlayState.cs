using TextLore.Shared.Models.Level;
using TextLore.Shared.Models.Player;

namespace TextLore.Shared.Logic;

public class PlayState(Level level, Player player)
{
    public Level Level { get; } = level;

    public Player Player { get; } = player;

    public Room? CurrentRoom => Level.GetRoom(Player.PositionInLevel);
}