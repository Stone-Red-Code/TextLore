using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

using TextLore.Console;
using TextLore.Games.Roguelike.Logic;
using TextLore.Games.Roguelike.Models;
using TextLore.Utilities.Logic;
using TextLore.Utilities.Models;

namespace TextLore.Games.Roguelike.Commands;

public class GenerateLevelCommand(IQueryable<RoomDefinition> roomDefinitions, NavigationManager navigationManager, Action<Level> levelGeneratedCallback) : Command
{
    private bool isGenerated = false;
    public override string Name => "generatelevel";
    public override string Description => "Generates a new level.";

    public override string[] Aliases => ["genlevel"];

    public override bool NoHistoryIfNoOutput => true;

    public override async Task<CommandResult> Execute(ConsoleWriter consoleOutput, string args)
    {
        if (!DeterministicRandom.TryParse(args, out DeterministicRandom? seed))
        {
            consoleOutput.WriteLine("Invalid seed. Generating a new one...");
            seed = await GenerateSeed();

            navigationManager.NavigateTo($"/roguelike/{seed.ToBase64()}");
        }

        consoleOutput.WriteLine("Generating level...");

        LevelGenerator levelGenerator = new LevelGenerator(seed, roomDefinitions);

        IProgress<PercentageProgress> progress = new Progress<PercentageProgress>(p => ReportProgress(p, consoleOutput));

        Level level;

        try
        {
            level = await levelGenerator.GenerateLevel(progress);
        }
        catch (InvalidOperationException ex)
        {
            return CommandResult.Failure(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return CommandResult.Failure(ex.Message);
        }

        isGenerated = true;

        consoleOutput.WriteLine($"Level size: {level.Size.Width}x{level.Size.Height}");
        consoleOutput.WriteLine($"Room count: {level.RoomCount}");
        consoleOutput.WriteLine($"Filled space: {(double)level.RoomCount / (level.Size.Width * level.Size.Height):F0}%");
        consoleOutput.WriteLine($"Seed: {seed.ToBase64()}");

        consoleOutput.WriteLine("Level generation complete!");

        levelGeneratedCallback?.Invoke(level);

        return CommandResult.Success("Level generated!");
    }

    private void ReportProgress(PercentageProgress progress, ConsoleWriter consoleOutput)
    {
        if (isGenerated)
        {
            return;
        }

        char spinner = (DateTime.UtcNow.Second % 4) switch
        {
            0 => '|',
            1 => '/',
            2 => '-',
            3 => '\\',
            _ => '|'
        };

        consoleOutput.ClearLine();

        consoleOutput.WriteLine($"{spinner} Generating level... {progress.Percentage}%");
    }

    private async Task<DeterministicRandom> GenerateSeed()
    {
        int baseSeed = Guid.NewGuid().GetHashCode();
        int min = await roomDefinitions.MinAsync(r => r.Id);
        int max = await roomDefinitions.MaxAsync(r => r.Id);

        return new DeterministicRandom(baseSeed, min, max);
    }
}