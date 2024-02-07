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

    public GameManager(Level level, IEnumerable<Type> typesToAdd, IEnumerable<object> objectsToAdd) : this(level)
    {
        foreach (Type type in typesToAdd)
        {
            scriptEngine.AddHostType(type.Name, type);
        }

        foreach (object obj in objectsToAdd)
        {
            scriptEngine.AddHostObject(obj.GetType().Name, obj);
        }
    }

    public GameManager(Level level, IEnumerable<Type> typesToAdd) : this(level)
    {
        foreach (Type type in typesToAdd)
        {
            scriptEngine.AddHostType(type.Name, type);
        }
    }

    public GameManager(Level level, IEnumerable<object> objectsToAdd) : this(level)
    {
        foreach (object obj in objectsToAdd)
        {
            scriptEngine.AddHostObject(obj.GetType().Name, obj);
        }
    }

    public virtual void StartGame()
    {
        SetPlayerPositionInLevel(PlayState.Level.StartRoom.Position);

        Size currentRoomSize = PlayState.CurrentRoom!.Definition.Size;

        SetPlayerPositionInRoom(new(currentRoomSize.Width / 2, currentRoomSize.Height / 2));
    }

    public void MovePlayerInLevel(Direction direction)
    {
        Position newPosition = PlayState.Player.PositionInLevel + direction;
        if (PlayState.Level.IsPositionInBounds(newPosition))
        {
            SetPlayerPositionInLevel(newPosition);
        }
    }

    public void MovePlayerInRoom(Direction direction)
    {
        Position newPosition = PlayState.Player.PositionInLevel + direction;
        if (PlayState.CurrentRoom?.IsPositionInBounds(newPosition) == true)
        {
            SetPlayerPositionInRoom(newPosition);
        }
    }

    public virtual void SetPlayerPositionInLevel(Position position)
    {
        PlayState.Player.PositionInLevel = position;

        SetScript(PlayState.CurrentRoom?.Definition.Script ?? string.Empty);

        _ = ExecuteMethod("onPlayerEnterRoom");
    }

    public virtual void SetPlayerPositionInRoom(Position position)
    {
        PlayState.Player.PositionInLevel = position;

        _ = ExecuteMethod("onPlayerMove");
    }

    public void Update()
    {
        _ = ExecuteMethod("onUpdate");
    }

    protected T? ExecuteMethod<T>(string method, ConsoleWriter? consoleWriter = null) where T : class
    {
        return ExecuteMethod(method, consoleWriter) as T;
    }

    protected object? ExecuteMethod(string method, ConsoleWriter? consoleWriter = null)
    {
        consoleWriter ??= new ConsoleWriter();

        try
        {
            return scriptEngine.Invoke(method, consoleWriter);
        }
        catch (ScriptEngineException ex)
        {
            consoleWriter.WriteLine(ex.Message);
        }
        catch (ScriptInterruptedException ex)
        {
            consoleWriter.WriteLine(ex.Message);
        }

        return default;
    }

    protected void SetScript(string script)
    {
        scriptEngine.Execute(script);
    }

    public sealed class ScriptContext(Func<Player> player, Func<RoomDefinition?> room)
    {
        public Player Player => player();
        public RoomDefinition? Room => room();
    }
}