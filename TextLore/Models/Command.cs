namespace TextLore.Models;

public abstract class Command
{
    public abstract string Name { get; }
    public abstract string Description { get; }
    public virtual string Usage { get; } = string.Empty;
    public virtual string[] Aliases { get; } = [];

    public abstract Task<bool> Execute(ConsoleOutput consoleOutput, string[] args);
}