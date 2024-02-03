namespace TextLore.Utilities.Models;

public readonly struct Direction(int x, int y)
{
    public static Direction Up => new Direction(0, -1);
    public static Direction Down => new Direction(0, 1);
    public static Direction Left => new Direction(-1, 0);
    public static Direction Right => new Direction(1, 0);
    public static Direction UpLeft => new Direction(-1, -1);
    public static Direction UpRight => new Direction(1, -1);
    public static Direction DownLeft => new Direction(-1, 1);
    public static Direction DownRight => new Direction(1, 1);
    public int X { get; } = x;
    public int Y { get; } = y;
}