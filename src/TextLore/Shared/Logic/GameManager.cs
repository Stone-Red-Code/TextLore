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

        ScriptContext currentScriptContext = new(() => PlayState.Player, () => PlayState.CurrentRoom);

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
        _ = SetPlayerPositionInLevel(PlayState.Level.StartRoom.Position);

        Size currentRoomSize = PlayState.CurrentRoom!.Definition.Size;

        _ = SetPlayerPositionInRoom(new(currentRoomSize.Width / 2, currentRoomSize.Height / 2));
    }

    public bool MovePlayerInLevel(Direction direction)
    {
        Position newPosition = PlayState.Player.PositionInLevel + direction;

        return SetPlayerPositionInLevel(newPosition);
    }

    public bool MovePlayerInRoom(Direction direction)
    {
        Position newPosition = PlayState.Player.PositionInLevel + direction;

        return SetPlayerPositionInRoom(newPosition);
    }

    public virtual bool SetPlayerPositionInLevel(Position position)
    {
        if (PlayState.Level.IsPositionInBounds(position))
        {
            return false;
        }

        PlayState.Player.PositionInLevel = position;

        SetScript(PlayState.CurrentRoom?.Definition.Script ?? string.Empty);

        _ = ExecuteMethod("onPlayerEnterRoom");

        return true;
    }

    public virtual bool SetPlayerPositionInRoom(Position position)
    {
        if (!PlayState.CurrentRoom?.IsPositionInBounds(position) == true)
        {
            return false;
        }

        PlayState.Player.PositionInLevel = position;

        _ = ExecuteMethod("onPlayerMove");

        return true;
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

    public sealed class ScriptContext(Func<Player> player, Func<Room?> room)
    {
        public Player Player => player();
        public Room? Room => room();
    }
}