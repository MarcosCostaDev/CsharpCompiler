using System.Text.Json;
namespace Rinha.Extensions;

internal static class JsonExtensions
{
    public static JsonSerializerOptions GetJsonDeserializerOptions()
    {
        return new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
        };
    }
}
