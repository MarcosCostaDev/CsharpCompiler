using System.Text.Json.Serialization;

namespace Rinha.Interpreter;

public abstract class FileLocator
{
    [JsonPropertyName("location")]
    public Location Location { get; set; }
}
