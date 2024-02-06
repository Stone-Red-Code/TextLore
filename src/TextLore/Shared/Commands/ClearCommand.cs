using TextLore.Console;

namespace TextLore.Shared.Commands;

public class ClearCommand : Command
{
    public override string Name => "clear";
    public override string Description => "Clears the console.";

    public override string[] Aliases => ["cls"];

    public override bool NoHistoryIfNoOutput => true;

    public override Task<CommandResult> Execute(ConsoleWriter consoleOutput, string args)
    {
        consoleOutput.ClearAll();
        return CommandResult.Success("Console cleared!");
    }
}