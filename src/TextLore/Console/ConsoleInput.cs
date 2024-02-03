namespace TextLore.Console;

public class ConsoleInput
{
    public string Text { get; set; } = string.Empty;
    public DateTime Time { get; } = DateTime.UtcNow;
}