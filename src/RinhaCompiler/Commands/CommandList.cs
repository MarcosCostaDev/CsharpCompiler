using System.CommandLine;

namespace RinhaCompiler.Commands;

internal static class CommandList
{
    public static RootCommand AddAllCommands(this RootCommand command)
    {
        command.AddCommand(InterpreterCommand.GetCommand());

        return command;
    }
}
