using TextLore.Models;

namespace TextLore.Commands;

public class ClearCommand : Command
{
    public override string Name => "clear";
    public override string Description => "Clears the console.";

    public override string[] Aliases => ["cls"];

    public override bool NoHistoryIfNoOutput => true;

    public override Task<CommandResult> Execute(ConsoleWriter consoleOutput, string args)
    {
        consoleOutput.Clear();
        return Task.FromResult(CommandResult.Success("Console cleared!"));
    }
}