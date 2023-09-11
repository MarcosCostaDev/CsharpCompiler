namespace RinhaCompiler.Interpreter;

public abstract class ValueExpression : Expression { }
public class ValueExpression<T> : ValueExpression
{
    public T Value { get; set; }

    public override object Run()
    {
        return Value;
    }
}
