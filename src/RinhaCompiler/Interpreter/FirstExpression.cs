namespace RinhaCompiler.Interpreter;

public sealed class FirstExpression : ValueExpression<Expression>
{
    public override object Run()
    {
        if (Value is TupleExpression tupleExpression)
        {
            tupleExpression.Scope = this;
            return tupleExpression.First.Run();
        }

        throw new ArgumentException($"Error on {nameof(FirstExpression)}.{nameof(Run)} -  {Location.GetLog()}");
    }
}
