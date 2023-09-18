using System.CommandLine;

namespace Rinha.Commands;

internal static class CommandList
{
    public static RootCommand AddAllCommands(this RootCommand rootCommand)
    {
        DefaultCommand(rootCommand);

        Interpreter(rootCommand);

        return rootCommand;
    }

    private static void DefaultCommand(RootCommand rootCommand)
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
    }

    private static void Interpreter(RootCommand rootCommand)
    {
        var fileArgument = new Argument<FileInfo>("file", "a AST.json file to be processed")
        {
            HelpName = "file AST.json"
        };

        var command = new Command("interpreter");
        command.AddAlias("i");
        command.AddArgument(fileArgument);

        command.SetHandler((fileArgumentValue) =>
        {
            Program.GlobalCommandManager.Invoke(new InterpreterCommand(fileArgumentValue.FullName));

        }, fileArgument);

        rootCommand.AddCommand(command);
    }

    private static void Parser(RootCommand rootCommand)
    {
        var fileArgument = new Argument<FileInfo>("file", "a .rinha file to be processed")
        {
            HelpName = "file .rinha"
        };

        var command = new Command("parser");
        command.AddAlias("p");
        command.AddArgument(fileArgument);

        var outputDirOption = new Option<DirectoryInfo?>("output-dir", "output directory");
        outputDirOption.AddAlias("o");

        command.SetHandler((fileArgumentValue, outputDirOptionValue) =>
        {
            Program.GlobalCommandManager.Invoke(new ParserCommand(fileArgumentValue.FullName, outputDirOptionValue));

        }, fileArgument, outputDirOption);

        rootCommand.AddCommand(command);
    }
}
