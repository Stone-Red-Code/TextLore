using TextLore.Console;

namespace TextLore.Shared.Commands;

public class EchoCommand : Command
{
    public override string Name => "echo";

    public override string Description => "Echoes the input.";

    public override Task<CommandResult> Execute(ConsoleWriter consoleOutput, string args)
    {
        consoleOutput.WriteLine(args);

        return CommandResult.Success();
    }
}