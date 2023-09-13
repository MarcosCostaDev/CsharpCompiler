using System.CommandLine;
using System.IO;

namespace RinhaCompiler.Commands;

internal static class CommandList
{
    public static RootCommand AddAllCommands(this RootCommand rootCommand)
    {
        var fileArgument = new Argument<FileInfo>("file", "a file to be processed")
        {
            HelpName = "file AST.json"
        };

        var interpreterOption = new Option<string>("-interpreter", "interpret the file selected") { IsRequired = false };
        interpreterOption.AddAlias("-i");
        rootCommand.AddArgument(fileArgument);
        rootCommand.AddGlobalOption(interpreterOption);

        rootCommand.SetHandler((fileArgumentValue, interpreterOptionValue) =>
        {
            Program.GlobalCommandManager.Invoke(new InterpreterCommand(fileArgumentValue.FullName));

        }, fileArgument, interpreterOption);

        return rootCommand;
    }
}
