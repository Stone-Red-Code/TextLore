namespace TextLore.Shared.Models;

public readonly struct Position(int x, int y)
{
    public int X => x;
    public int Y => y;

    public static Position operator +(Position a, Position b)
    {
        return new Position(a.X + b.X, a.Y + b.Y);
    }

    public static Position operator -(Position a, Position b)
    {
        return new Position(a.X - b.X, a.Y - b.Y);
    }

    public static Position operator +(Position a, Direction b)
    {
        return new Position(a.X + b.X, a.Y + b.Y);
    }

    public static Position operator -(Position a, Direction b)
    {
        return new Position(a.X - b.X, a.Y - b.Y);
    }
}