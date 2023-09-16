using System.Reflection;

namespace Rinha.Test.Managers;

internal class FileManager
{
    public static string GetTextFromFiles(string relativePath)
    {
        return File.ReadAllText(GetFullPath(relativePath));
    }

    public static string GetFullPath(string relativePath)
    {
        var codeBaseUrl = new Uri(Assembly.GetExecutingAssembly().Location);
        var codeBasePath = Uri.UnescapeDataString(codeBaseUrl.AbsolutePath);
        var dirPath = Path.GetDirectoryName(codeBasePath);
        return Path.Combine(dirPath, "Files", relativePath);
    }
}
