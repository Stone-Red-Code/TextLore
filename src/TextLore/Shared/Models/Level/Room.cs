namespace TextLore.Shared.Models.Level;

public class Room(Position position, RoomDefinition roomDefinition)
{
    private readonly Dictionary<Position, IRoomObject> objects = [];
    public Position Position => position;
    public List<Direction> DoorDirections { get; init; } = [Direction.Up, Direction.Down, Direction.Right, Direction.Left];
    public RoomDefinition Definition => roomDefinition;

    public IRoomObject? GetObject(Position point)
    {
        return objects.GetValueOrDefault(point);
    }

    public IRoomObject? GetObject(string key)
    {
        return objects.Values.FirstOrDefault(x => x.Key == key);
    }

    public IReadOnlyDictionary<Position, IRoomObject> GetObjects()
    {
        return objects;
    }

    public IReadOnlyDictionary<Position, T> GetObjects<T>() where T : IRoomObject
    {
        return objects
            .Where(x => x.Value is T)
            .ToDictionary(x => x.Key, x => (T)x.Value);
    }

    public bool AddObject(Position point, IRoomObject roomObject)
    {
        if (!IsPositionInBounds(point))
        {
            return false;
        }

        objects[point] = roomObject;

        return true;
    }

    public bool IsPositionInBounds(Position point)
    {
        return point.X >= 0 && point.X < roomDefinition.Size.Width && point.Y >= 0 && point.Y < roomDefinition.Size.Height;
    }

    public bool ObjectExists(Position point)
    {
        return objects.ContainsKey(point);
    }

    public void RemoveObject(Position point)
    {
        _ = objects.Remove(point);
    }

    public void ClearObjects()
    {
        objects.Clear();
    }
}