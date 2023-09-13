using System.Text.Json.Serialization;

namespace RinhaCompiler.Interpreter;

public abstract class FileLocator
{
    [JsonPropertyName("location")]
    public Location Location { get; set; }
}
