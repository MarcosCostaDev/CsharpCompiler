using System.Text.Json.Serialization;

namespace RinhaCompiler.Interpreter;

public abstract class FileLocator
{
    [JsonPropertyName("location")]
    public Location Location { get; set; }

    public TExpression ShallowCopy<TExpression>() where TExpression : Expression
    {
        return (TExpression)this.MemberwiseClone();
    }

    public object ShallowCopyAnnon() 
    {
        return this.MemberwiseClone();
    }
}
