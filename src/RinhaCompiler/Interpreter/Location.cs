using System.Text.Json.Serialization;

namespace Rinha.Interpreter;

public class Location
{
    public int Start { get; set; }
    public int End { get; set; }

    [JsonPropertyName("filename")]
    public string FileName { get; set; }

    public string GetLog() => $"Processing file {FileName}, start: {Start}, end {End}";
}
