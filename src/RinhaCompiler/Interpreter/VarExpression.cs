namespace RinhaCompiler.Interpreter;

public sealed class VarExpression : Expression
{
    public string Text { get; set; }

    public override object Run()
    {
        return Text;
    }

}
