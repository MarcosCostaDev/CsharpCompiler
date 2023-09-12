using RinhaCompiler.Commands.Abstracts;
using RinhaCompiler.Extensions;
using RinhaCompiler.Interpreter;
using System.CommandLine;
using System.Text.Json;

namespace RinhaCompiler.Commands;

internal sealed class InterpreterCommand : AbstractCommand
{
    private readonly string _jsonFile;

    public InterpreterCommand(string jsonFile)
    {
        _jsonFile = jsonFile;
    }

    public override async Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var compiledFile = await GetCompiledFile(cancellationToken);

        compiledFile.Run();

    }

    private async Task<CompiledFile?> GetCompiledFile(CancellationToken cancellationToken)
    {
        using var stream = new FileStream(_jsonFile, FileMode.Open, FileAccess.Read);
        return await JsonSerializer.DeserializeAsync<CompiledFile?>(stream, JsonExtensions.GetJsonDeserializerOptions(), cancellationToken: cancellationToken);
    }

    internal static Command GetCommand()
    {
        var command = new Command("interpreter", "Interpreter json file.");

        command.AddAlias("i");

        var fileOption = new Option<string>("--file", "Your file .json to interpreter");
        fileOption.AddAlias("-f");

        command.AddOption(fileOption);

        command.SetHandler(async (fileOption) =>
        {
            Program.GlobalCommandManager.Invoke(new InterpreterCommand(fileOption));
        }, fileOption);

        return command;
    }
}
