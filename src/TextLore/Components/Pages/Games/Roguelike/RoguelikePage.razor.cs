using Microsoft.AspNetCore.Components;

using TextLore.Console;
using TextLore.Shared.Commands;
using TextLore.Shared.Logic;
using TextLore.Shared.Models.Level;

namespace TextLore.Components.Pages.Games.Roguelike;

public partial class RoguelikePage
{
    private readonly List<Command> commands = [new TestCommand(), new ClearCommand()];

    // Short description how to play the game
    private readonly string desctiption = "This is a roguelike game. Use commands to play the game.";

    private Shared.Console console = null!;
    private HelpCommand? helpCommand;

    private GameManager? gameManager = null;

    [Parameter]
    public string Seed { get; set; } = string.Empty;

    public async void OnLevelGenerated(Level level)
    {
        gameManager = new GameManager(level);

        StateHasChanged();
    }

    protected override void OnInitialized()
    {
        helpCommand = new HelpCommand(commands);
        commands.Add(helpCommand);
        commands.Add(new ExitCommand(NavigationManager));
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await console.Execute(new GenerateLevelCommand("roguelike", DatabaseContext.Rooms, NavigationManager, OnLevelGenerated), Seed);
        }
    }
}