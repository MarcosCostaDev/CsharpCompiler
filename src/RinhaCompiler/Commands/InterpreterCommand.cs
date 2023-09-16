using Rinha.Commands.Abstracts;
using Rinha.Extensions;
using Rinha.Interpreter;
using System.Text.Json;

namespace Rinha.Commands;

internal sealed class InterpreterCommand : AbstractCommand
{
    private readonly string _jsonFile;

    public InterpreterCommand(string jsonFile)
    {
        _jsonFile = jsonFile;
    }

    public override bool CanExecute()
    {
        var exist = File.Exists(_jsonFile);
        if(!exist)
        {
            Console.WriteLine($"The file {_jsonFile} don't exist");
        }
        return exist;
    }

    public override void Execute()
    {
        var compiledFile = GetCompiledFile();

        compiledFile.Run();
    }

    private CompiledFile? GetCompiledFile()
    {
        using var stream = new FileStream(_jsonFile, FileMode.Open, FileAccess.Read);
        return JsonSerializer.Deserialize<CompiledFile?>(stream, JsonExtensions.GetJsonDeserializerOptions());
    }

}
