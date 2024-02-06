namespace TextLore.Shared.Models;

public readonly struct Size(int width, int height)
{
    public int Width => width;
    public int Height => height;
}