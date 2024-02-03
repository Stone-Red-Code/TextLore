using Microsoft.AspNetCore.Components;

using TextLore.Console;
using TextLore.Console.Commands;
using TextLore.Games.Roguelike.Commands;
using TextLore.Games.Roguelike.Models;

namespace TextLore.Games.Roguelike;

public partial class RoguelikePage
{
    private readonly List<Command> commands = [new TestCommand(), new ClearCommand()];

    // Short description how to play the game
    private readonly string desctiption = "This is a roguelike game. Use commands to play the game.";

    private Components.Shared.Console console = null!;
    private HelpCommand? helpCommand;

    private Level? level;

    [Parameter]
    public string Seed { get; set; } = string.Empty;

    public void OnLevelGenerated(Level level)
    {
        this.level = level;
    }

    protected override void OnInitialized()
    {
        helpCommand = new HelpCommand(commands);
        commands.Add(helpCommand);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await console.Execute(new GenerateLevelCommand(DatabaseContext.RoguelikeRooms, NavigationManager, OnLevelGenerated), Seed);
        }
    }
}