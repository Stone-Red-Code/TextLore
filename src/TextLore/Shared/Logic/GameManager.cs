using Microsoft.ClearScript;
using Microsoft.ClearScript.V8;

using TextLore.Console;
using TextLore.Shared.Models;
using TextLore.Shared.Models.Level;
using TextLore.Shared.Models.Player;

namespace TextLore.Shared.Logic;

public partial class GameManager
{
    private readonly V8ScriptEngine scriptEngine = new V8ScriptEngine();

    public PlayState PlayState { get; }

    public GameManager(Level level)
    {
        PlayState = new(level, new());

        ScriptContext currentScriptContext = new(() => PlayState.Player, () => PlayState.CurrentRoom?.Definition);

        scriptEngine.AddHostObject("context", currentScriptContext);
    }

    public void MovePlayer(Direction direction)
    {
        Position newPosition = PlayState.Player.Position + direction;
        if (PlayState.Level.IsPositionInBounds(newPosition))
        {
            PlayState.Player.Position = newPosition;

            SetScript(PlayState.CurrentRoom?.Definition.Script ?? string.Empty);
        }
    }

    public void ExecuteMethod(string method, ConsoleWriter consoleWriter)
    {
        try
        {
            _ = scriptEngine.Invoke(method, consoleWriter);
        }
        catch (ScriptEngineException ex)
        {
            consoleWriter.WriteLine(ex.Message);
        }
        catch (ScriptInterruptedException ex)
        {
            consoleWriter.WriteLine(ex.Message);
        }
    }

    private void SetScript(string script)
    {
        scriptEngine.Execute(script);
    }

    public sealed class ScriptContext(Func<Player> player, Func<RoomDefinition?> room)
    {
        public Player Player => player();
        public RoomDefinition? Room => room();
    }
}