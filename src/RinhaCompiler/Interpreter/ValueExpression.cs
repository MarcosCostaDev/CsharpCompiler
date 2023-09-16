namespace Rinha.Interpreter;

public abstract class ValueExpression : Expression { }
public class ValueExpression<T> : ValueExpression
{
    public T Value { get; set; }

    public override object Run()
    {
        return Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
