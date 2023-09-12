namespace RinhaCompiler.Interpreter;

public sealed class ConditionExpression : Expression
{
    public Expression Condition { get; set; }
    public Expression Then { get; set; }
    public Expression Otherwise { get; set; }

    public override object Run()
    {
        Condition.Scope = this;

        if (Condition is ValueExpression<bool> actualValue && actualValue.Value ||
           Condition.Run() is ValueExpression<bool> conditionResult && conditionResult.Value)
        {
            Then.Scope = this;
            return Then.Run();
        }
        else if (Otherwise != null)
        {
            Otherwise.Scope = this;
            return Otherwise.Run();
        }
        throw new ArgumentException($"Error on {nameof(ConditionExpression)} - {Location.GetLog()}");
    }
}
