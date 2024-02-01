namespace TextLore.Models;

public class CommandResult(string message, bool success)
{
    public string Message { get; } = message;
    public bool IsSuccess { get; } = success;

    public static CommandResult Success(string message = "")
    {
        return new(message, true);
    }

    public static CommandResult Failure(string message = "")
    {
        return new(message, false);
    }

    public static implicit operator CommandResult(string message)
    {
        return Success(message);
    }
}