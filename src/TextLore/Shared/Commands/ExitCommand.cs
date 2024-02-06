using Microsoft.AspNetCore.Components;

using TextLore.Console;

namespace TextLore.Shared.Commands;

public class ExitCommand(NavigationManager navigationManager) : Command
{
    public override string Name => "exit";

    public override string Description => "Exits the game.";

    public override Task<CommandResult> Execute(ConsoleWriter consoleOutput, string args)
    {
        consoleOutput.WriteLine("Exiting the game...");

        navigationManager.NavigateTo("/");

        return CommandResult.Success();
    }
}