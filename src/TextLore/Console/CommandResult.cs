namespace TextLore.Console;

public class CommandResult(string message, bool success, Command? precedingCommand = null)
{
    public string Message { get; } = message;
    public bool IsSuccess { get; } = success;

    public Command? PrecedingCommand { get; } = precedingCommand;

    public static CommandResult Success(string message = "")
    {
        return new(message, true);
    }

    public static CommandResult Success(Command precedingCommand, string message = "")
    {
        return new(message, true, precedingCommand);
    }

    public static CommandResult Failure(string message = "")
    {
        return new(message, false);
    }

    public static CommandResult Failure(Command precedingCommand, string message = "")
    {
        return new(message, false, precedingCommand);
    }

    public static implicit operator CommandResult(string message)
    {
        return Success(message);
    }

    public static implicit operator Task<CommandResult>(CommandResult result)
    {
        return Task.FromResult(result);
    }
}