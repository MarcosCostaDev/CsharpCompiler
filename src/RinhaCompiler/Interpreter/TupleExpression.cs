namespace Rinha.Interpreter;

public sealed class TupleExpression : Expression
{
    public Expression First { get; set; }
    public Expression Second { get; set; }
    public override object Run()
    {
        First.Scope = this;
        Second.Scope = this;
        return (First.Run(), Second.Run());
    }
}
