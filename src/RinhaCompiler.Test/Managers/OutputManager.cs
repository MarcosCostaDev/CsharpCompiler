using System.Text;
namespace Rinha.Test.Managers;

public class RedirectOutput : TextWriter
{
    private readonly ITestOutputHelper _output;

    public RedirectOutput(ITestOutputHelper output)
    {
        _output = output;
    }

    public override Encoding Encoding => Encoding.UTF8;

    public override void Write(char[] buffer, int index, int count)
    {
        _output.WriteLine(new string(buffer, index, count));
    }
}
