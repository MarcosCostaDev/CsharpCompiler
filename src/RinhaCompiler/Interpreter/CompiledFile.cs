using System.Text.Json.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace RinhaCompiler.Interpreter;

public interface ICommandExecute
{
    object Run();
}
public class Location
{
    public int Start { get; set; }
    public int End { get; set; }

    [JsonPropertyName("filename")]
    public string FileName { get; set; }
}

public abstract class FileLocator
{
    [JsonPropertyName("location")]
    public Location Location { get; set; }
}


public class CompiledFile : FileLocator, ICommandExecute
{
    public string Name { get; set; }

    public Expression Expression { get; set; }

    public object Run()
    {
       return Expression.Run();
    }
}

[JsonPolymorphic(TypeDiscriminatorPropertyName = "kind", IgnoreUnrecognizedTypeDiscriminators = true)]
[JsonDerivedType(typeof(PrintExpression), typeDiscriminator: nameof(Term.Print))]
[JsonDerivedType(typeof(CallExpression), typeDiscriminator: nameof(Term.Call))]
[JsonDerivedType(typeof(FunctionExpression), typeDiscriminator: nameof(Term.Function))]
[JsonDerivedType(typeof(ConditionExpression), typeDiscriminator: nameof(Term.If))]
[JsonDerivedType(typeof(VarExpression), typeDiscriminator: nameof(Term.Var))]
[JsonDerivedType(typeof(BinaryExpression), typeDiscriminator: nameof(Term.Binary))]
[JsonDerivedType(typeof(LetExpression), typeDiscriminator: nameof(Term.Let))]
[JsonDerivedType(typeof(ValueExpression<string>), typeDiscriminator: nameof(Term.Str))]
[JsonDerivedType(typeof(ValueExpression<int>), typeDiscriminator: nameof(Term.Int))]
[JsonDerivedType(typeof(ValueExpression<bool>), typeDiscriminator: nameof(Term.Bool))]
public abstract class Expression : FileLocator, ICommandExecute
{
    public abstract object Run();
}

public sealed class VarExpression : Expression
{
    public string Text { get; set; }

    public override object Run()
    {
        return Text;
    }
}

public sealed class LetExpression : Expression
{
    public VarExpression Name { get; set; }
    public Expression Next { get; set; }

    public override object Run()
    {
        throw new NotImplementedException();
    }
}

public class ValueExpression<T> : Expression
{
    public T Value { get; set; }

    public override object Run()
    {
        return Value;
    }
}

public sealed class PrintExpression : ValueExpression<Expression>
{
    public override object Run()
    {
        if(Value.Run() is bool boolValue)
        {
            Console.WriteLine(boolValue);
        }
        else if (Value.Run() is string strValue)
        {
            Console.WriteLine(strValue);
        }
        else if(Value.Run() is int intValue)
        {
            Console.WriteLine(intValue);
        }
        return null!;
    }
}

public sealed class FunctionExpression : ValueExpression<Expression>
{
    public VarExpression[] Parameters { get; set; }
}

public sealed class ConditionExpression : ValueExpression<Expression>
{
    public Expression Condition { get; set; }
    public Expression Then { get; set; }
    public Expression Otherwise { get; set; }
}

public sealed class BinaryExpression : Expression
{
    [JsonPropertyName("lhs")]
    public VarExpression LeftHandSide { get; set; }
    [JsonPropertyName("rhs")]
    public VarExpression RightHandSide { get; set; }

    [JsonPropertyName("op")]
    public BinaryOperator Operation { get; set; }

    public override object Run()
    {
        throw new NotImplementedException();
    }
}

public sealed class CallExpression : Expression
{
    public Expression Callee { get; set; }

    public override object Run()
    {
        throw new NotImplementedException();
    }
}