using Rinha.Commands;
using Rinha.Commands.Manager;
using System.CommandLine;

var description = @"

 Welcome to my first ever interpreter that read and process AST.json from .rinha files. in C# 
 Developer: Marcos Costa
 Github: https://github.com/MarcosCostaDev

 #csharp #dotnet-core #rinha-de-compilers
 #rinha-de-compiler #rinha-de-compiladores";

var rootCommand = new RootCommand(description);

rootCommand.AddAllCommands();

rootCommand.Invoke(args);

internal partial class Program
{
    internal static CommandManager GlobalCommandManager = new();
    internal static void Main(params string[] args) { }
}

