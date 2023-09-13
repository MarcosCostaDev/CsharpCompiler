namespace RinhaCompiler.Interpreter;

public sealed class VarExpression : Expression
{
    public string Text { get; set; }

    public override object Run()
    {
       return GetValue();
    }

    public Expression GetValue()
    {
        return RunUtil.FindVariableValue(this, Text);
    }

    public Expression SetValue(Expression value)
    {
        return RunUtil.FindAndCreateOrUpdateScopedVariableValue(this, Text, value);
    }

}
