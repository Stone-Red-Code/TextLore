using TextLore.Utilities.Logic;
using TextLore.Utilities.Models;

namespace TextLore.Games.Roguelike.Models;

public class Level(Size size, DeterministicRandom seed)
{
    private readonly Dictionary<Position, Room> rooms = [];

    public Size Size => size;
    public int RoomCount => rooms.Count;

    public DeterministicRandom Seed => seed;

    public Room? GetRoom(Position point)
    {
        return rooms.GetValueOrDefault(point);
    }

    public IReadOnlyDictionary<Position, Room> GetRooms()
    {
        return rooms;
    }

    public void AddRoom(Room room)
    {
        if (!IsRoomInBounds(room))
        {
            throw new InvalidOperationException("Room is out of bounds.");
        }

        rooms[room.Position] = room;
    }

    public bool IsPositionInBounds(Position point)
    {
        return point.X >= 0 && point.X < size.Width && point.Y >= 0 && point.Y < size.Height;
    }

    public bool IsRoomInBounds(Room room)
    {
        return IsPositionInBounds(room.Position);
    }

    public bool RoomExists(Position point)
    {
        return rooms.ContainsKey(point);
    }

    public void ClearRoom(Position point)
    {
        _ = rooms.Remove(point);
    }

    public void ClearRooms()
    {
        rooms.Clear();
    }
}