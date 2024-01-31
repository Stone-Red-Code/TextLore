namespace TextLore.Models;

public class ConsoleOutputEventArgs(string message, ConsoleMessageType messageType) : EventArgs
{
    public string Message { get; } = message;

    public ConsoleMessageType MessageType { get; } = messageType;
}