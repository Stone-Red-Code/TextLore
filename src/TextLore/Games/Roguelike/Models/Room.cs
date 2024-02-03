using TextLore.Utilities.Models;

namespace TextLore.Games.Roguelike.Models;

public class Room(Position position, RoomDefinition roomDefinition)
{
    public Position Position => position;
    public List<Direction> DoorDirections { get; init; } = [Direction.Up, Direction.Down, Direction.Right, Direction.Left];
    public RoomDefinition Definition => roomDefinition;
}