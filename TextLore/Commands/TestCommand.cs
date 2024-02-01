using TextLore.Models;

namespace TextLore.Commands;

public class TestCommand : Command
{
    public override string Name => "test";
    public override string Description => "A test command.";

    public override string[] Aliases => ["tst"];

    public override Task<CommandResult> Execute(ConsoleWriter consoleOutput, string args)
    {
        consoleOutput.WriteLine("This is a test command.");
        return Task.FromResult(CommandResult.Success("test"));
    }
}