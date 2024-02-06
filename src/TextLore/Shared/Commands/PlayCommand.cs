using Microsoft.AspNetCore.Components;

using TextLore.Console;

namespace TextLore.Shared.Commands;

public class PlayCommand(NavigationManager navigationManager, params string[] gameNames) : Command
{
    public override string Name => "play";

    public override string Description => "Starts a new game.";

    public override string Usage => "play <game> (You only have to type the first few letters of the game name.)";

    public override Task<CommandResult> Execute(ConsoleWriter consoleOutput, string args)
    {
        if (string.IsNullOrWhiteSpace(args))
        {
            consoleOutput.WriteLine("Available games:");

            foreach (string availableGame in gameNames)
            {
                consoleOutput.WriteLine($"- {availableGame}");
            }

            return Task.FromResult(CommandResult.Failure("You need to specify a game to play."));
        }

        string? game = Array.Find(gameNames, g => g.StartsWith(args, StringComparison.CurrentCultureIgnoreCase));

        if (game is null)
        {
            consoleOutput.WriteLine("Available games:");

            foreach (string availableGame in gameNames)
            {
                consoleOutput.WriteLine($"- {availableGame}");
            }

            return Task.FromResult(CommandResult.Failure($"Game '{args}' not found."));
        }

        consoleOutput.WriteLine($"Starting game '{game}'...");

        navigationManager.NavigateTo($"/{game}");

        return Task.FromResult(CommandResult.Success());
    }
}