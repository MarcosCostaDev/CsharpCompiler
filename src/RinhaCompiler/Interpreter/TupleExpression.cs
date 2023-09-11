namespace RinhaCompiler.Interpreter;

public sealed class TupleExpression : Expression
{
    public Expression First { get; set; }
    public Expression Second { get; set; }
    public override object Run()
    {
        First.Parent = this;
        Second.Parent = this;
        return (First.Run(), Second.Run());
    }
}
