namespace TextLore.Console.Commands;

public class HelpCommand(IEnumerable<Command> commands) : Command
{
    public override string Name => "help";
    public override string Description => "Displays a list of commands and their descriptions.";
    public override string Usage => "help <command>";
    public override string[] Aliases => new[] { "h", "?" };

    public override Task<CommandResult> Execute(ConsoleWriter consoleOutput, string args)
    {
        if (string.IsNullOrWhiteSpace(args))
        {
            consoleOutput.WriteLine("Available commands:");
            foreach (Command command in commands)
            {
                consoleOutput.WriteLine($"  {command.Name} - {command.Description}");
            }
            return CommandResult.Success();
        }
        else
        {
            Command? command = commands.FirstOrDefault(c => c.Name.Equals(args, StringComparison.OrdinalIgnoreCase) || c.Aliases.Contains(args, StringComparer.OrdinalIgnoreCase));
            if (command is null)
            {
                consoleOutput.WriteLine($"Command \"{args[0]}\" not found.");
                return CommandResult.Failure();
            }
            else
            {
                consoleOutput.WriteLine($"Command: {command.Name}");

                if (command.Aliases.Length > 0)
                {
                    consoleOutput.WriteLine($"Aliases: {string.Join(", ", command.Aliases)}");
                }

                consoleOutput.WriteLine($"Description: {command.Description}");

                if (!string.IsNullOrWhiteSpace(command.Usage))
                {
                    consoleOutput.WriteLine($"Usage: {command.Usage}");
                }

                return CommandResult.Success();
            }
        }
    }
}