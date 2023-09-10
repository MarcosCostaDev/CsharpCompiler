using RinhaCompiler.Commands;
using RinhaCompiler.Commands.Manager;
using System.CommandLine;

var rootCommand = new RootCommand("Welcome to my first ever interpreter for .rinha files. \n #rinha-de-compiladores");

rootCommand.AddAllCommands();

rootCommand.Invoke(args);

internal partial class Program {
    internal static CommandManager GlobalCommandManager = new();
}

