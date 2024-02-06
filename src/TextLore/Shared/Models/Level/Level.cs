using TextLore.Shared.Logic;
using TextLore.Shared.Models;

namespace TextLore.Shared.Models.Level;

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

    public bool AddRoom(Room room)
    {
        if (!IsRoomInBounds(room))
        {
            return false;
        }

        if (room.Definition.Tag == RoomTag.Unique && RoomExists(room.Definition))
        {
            return false;
        }

        if (room.Definition.Tag == RoomTag.Start && rooms.Values.Any(r => r.Definition.Tag == RoomTag.Start))
        {
            return false;
        }

        rooms[room.Position] = room;

        return true;
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

    public bool RoomExists(RoomDefinition definition)
    {
        return rooms.Values.Any(r => r.Definition.Id == definition.Id);
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