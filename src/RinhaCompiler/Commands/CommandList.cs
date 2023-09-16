using System.CommandLine;

namespace Rinha.Commands;

internal static class CommandList
{
    public static RootCommand AddAllCommands(this RootCommand rootCommand)
    {
        var fileArgument = new Argument<FileInfo>("file", "a file to be processed")
        {
            HelpName = "file AST.json"
        };

        rootCommand.AddArgument(fileArgument);

        rootCommand.SetHandler((fileArgumentValue) =>
        {
            Program.GlobalCommandManager.Invoke(new InterpreterCommand(fileArgumentValue.FullName));

        }, fileArgument);

        return rootCommand;
    }
}
