namespace TextLore.Console;

public class ConsoleOutput
{
    public string Text { get; set; } = string.Empty;
    public string Command { get; set; } = string.Empty;
    public DateTime Time { get; } = DateTime.UtcNow;
}