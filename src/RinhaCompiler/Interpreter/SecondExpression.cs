namespace Rinha.Interpreter;

public sealed class SecondExpression : ValueExpression<Expression>
{
    public override object Run()
    {
        if (Value is TupleExpression tupleExpression)
        {
            tupleExpression.Scope = this;
            return tupleExpression.Second.Run();
        }

        throw new ArgumentException($"Error on  {nameof(SecondExpression)}.{nameof(Run)} - {Location.GetLog()}");
    }
}
