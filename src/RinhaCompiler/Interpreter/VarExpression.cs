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
        return RunUntil.FindVariableValue(this, Text);
    }

    public Expression SetValue(Expression value)
    {
        return RunUntil.FindAndUpdateVariableValue(this, Text, value);
    }

}
