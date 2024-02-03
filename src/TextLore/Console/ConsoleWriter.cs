namespace TextLore.Console;

public class ConsoleWriter
{
    public event EventHandler<ConsoleOutputEventArgs>? OnOutput;

    public event EventHandler<EventArgs>? OnClearAll;

    public event EventHandler<EventArgs>? OnClearLine;

    public event EventHandler<EventArgs>? OnClearCurrent;

    public void Write(string message, ConsoleMessageType consoleMessageType = ConsoleMessageType.Default)
    {
        OnOutput?.Invoke(this, new(message, consoleMessageType));
    }

    public void WriteLine(string message)
    {
        Write(message + Environment.NewLine);
    }

    public void WriteError(string message)
    {
        Write($"[ERROR] {message}", ConsoleMessageType.Error);
    }

    public void WriteErrorLine(string message)
    {
        WriteError(message + Environment.NewLine);
    }

    public void WriteWarning(string message)
    {
        Write($"[WARNING] {message}", ConsoleMessageType.Warning);
    }

    public void WriteWarningLine(string message)
    {
        WriteWarning(message + Environment.NewLine);
    }

    public void WriteInfo(string message)
    {
        Write($"[INFO] {message}", ConsoleMessageType.Info);
    }

    public void WriteInfoLine(string message)
    {
        WriteInfo(message + Environment.NewLine);
    }

    public void ClearLine()
    {
        OnClearLine?.Invoke(this, EventArgs.Empty);
    }

    public void ClearAll()
    {
        OnClearAll?.Invoke(this, EventArgs.Empty);
    }

    public void ClearCurrent()
    {
        OnClearCurrent?.Invoke(this, EventArgs.Empty);
    }
}