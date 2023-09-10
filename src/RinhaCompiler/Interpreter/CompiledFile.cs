using System.Net;
using System.Text.Json.Serialization;

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

    public string GetLog() => $"Processing file {FileName}, start: {Start}, end {End}";
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
[JsonDerivedType(typeof(VarExpression), typeDiscriminator: nameof(Term.Var))]
[JsonDerivedType(typeof(FunctionExpression), typeDiscriminator: nameof(Term.Function))]
[JsonDerivedType(typeof(CallExpression), typeDiscriminator: nameof(Term.Call))]
[JsonDerivedType(typeof(LetExpression), typeDiscriminator: nameof(Term.Let))]
[JsonDerivedType(typeof(ValueExpression<string>), typeDiscriminator: nameof(Term.Str))]
[JsonDerivedType(typeof(ValueExpression<int>), typeDiscriminator: nameof(Term.Int))]
[JsonDerivedType(typeof(ValueExpression<bool>), typeDiscriminator: nameof(Term.Bool))]
[JsonDerivedType(typeof(BinaryExpression), typeDiscriminator: nameof(Term.Binary))]
[JsonDerivedType(typeof(ConditionExpression), typeDiscriminator: nameof(Term.If))]
[JsonDerivedType(typeof(TupleExpression), typeDiscriminator: nameof(Term.Tuple))]
[JsonDerivedType(typeof(FirstExpression), typeDiscriminator: nameof(Term.First))]
[JsonDerivedType(typeof(SecondExpression), typeDiscriminator: nameof(Term.Second))]
[JsonDerivedType(typeof(PrintExpression), typeDiscriminator: nameof(Term.Print))]
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
    public Expression Value { get; set; }
    public override object Run()
    {
        Value?.Run();

        return Next.Run();
    }
}

public abstract class ValueExpression : Expression { }
public class ValueExpression<T> : ValueExpression
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
        var runResult = Value.Run();
        if (runResult is bool boolValue)
        {
            Console.WriteLine(boolValue);
        }
        else if (runResult is string strValue)
        {
            Console.WriteLine(strValue);
        }
        else if (runResult is int intValue)
        {
            Console.WriteLine(intValue);
        }
        else if (runResult is Expression expressionValue)
        {
            if (expressionValue is PrintExpression) throw new ArgumentException($"Error on processing {Location.GetLog()}");
            if (expressionValue is TupleExpression tupleExpression)
            {
                Console.WriteLine($"({tupleExpression.First.Run()}, {tupleExpression.Second.Run()})");
                return null!;
            }

            Console.WriteLine(expressionValue.Run());
        }
        return null!;
    }
}

public sealed class FunctionExpression : ValueExpression<Expression>
{
    public IEnumerable<Expression> Parameters { get; set; }

    public override object Run()
    {
        return Value.Run();
    }
}

public sealed class TupleExpression : Expression
{
    public Expression First { get; set; }
    public Expression Second { get; set; }
    public override object Run()
    {
        return (First.Run(), Second.Run());
    }
}

public sealed class FirstExpression : ValueExpression<Expression>
{
    public override object Run()
    {
        if (Value is TupleExpression tupleExpression)
        {
            return tupleExpression.First.Run();
        }

        throw new ArgumentException($"Error on processing {Location.GetLog()}");
    }
}

public sealed class SecondExpression : ValueExpression<Expression>
{
    public override object Run()
    {
        if (Value is TupleExpression tupleExpression)
        {
            return tupleExpression.Second.Run();
        }

        throw new ArgumentException($"Error on processing {Location.GetLog()}");
    }
}


public sealed class ConditionExpression : ValueExpression<Expression>
{
    public Expression Condition { get; set; }
    public Expression Then { get; set; }
    public Expression Otherwise { get; set; }

    public override object Run()
    {
        if (Condition is ValueExpression<bool> conditionResult)
        {
            if (conditionResult.Value)
            {
                return Then.Run();
            }
            return Otherwise?.Run();
        }

        throw new ArgumentException($"Error on processing {Condition.Location.GetLog()}");
    }
}

public sealed class BinaryExpression : Expression
{
    [JsonPropertyName("lhs")]
    public Expression LeftHandSide { get; set; }

    [JsonPropertyName("rhs")]
    public Expression RightHandSide { get; set; }

    [JsonPropertyName("op")]
    public string Operation { get; set; }

    private BinaryOperator GetOperation() => (BinaryOperator)Enum.Parse(typeof(BinaryOperator), Operation, true);

    public override object Run()
    {
        var leftSide = RunUntil.Find<ValueExpression>(LeftHandSide);
        var rightSide = RunUntil.Find<ValueExpression>(RightHandSide);

        switch (GetOperation())
        {
            case BinaryOperator.Add:
                return AddOperation(leftSide, rightSide);
            case BinaryOperator.Sub:
                return SubOperation(leftSide, rightSide);
            case BinaryOperator.Mul:
                return MultiOperation(leftSide, rightSide);
            case BinaryOperator.Div:
                return DivOperation(leftSide, rightSide);
            case BinaryOperator.Rem:
                return RemOperation(leftSide, rightSide);
            case BinaryOperator.Eq:
                return EqualOperation(leftSide, rightSide);
            case BinaryOperator.Neq:
                return NotEqualOperation(leftSide, rightSide);
            case BinaryOperator.Lt:
                return LessThanOperation(leftSide, rightSide);
            case BinaryOperator.Gt:
                return GreaterThanOperation(leftSide, rightSide);
            case BinaryOperator.Lte:
                return LessOrEqualThanOperation(leftSide, rightSide);
            case BinaryOperator.Gte:
                return GreaterOrEqualThanOperation(leftSide, rightSide);
            case BinaryOperator.And:
                return AndOperation(leftSide, rightSide);
            case BinaryOperator.Or:
                return OrOperation(leftSide, rightSide);
            default:
                throw new ArgumentException($"Error on processing near {leftSide.Location.GetLog()}");
        }

    }

    private Expression AddOperation(ValueExpression leftSide, ValueExpression rightSide)
    {
        if (leftSide is ValueExpression<int> leftSideIntValue)
        {
            if (rightSide is ValueExpression<int> rightSideIntValue)
            {
                return new ValueExpression<int> { Value = leftSideIntValue.Value + rightSideIntValue.Value, Location = leftSide.Location };
            }
            else if (rightSide is ValueExpression<string> rightSideStrValue)
            {
                return new ValueExpression<string> { Value = leftSideIntValue.Value + rightSideStrValue.Value, Location = leftSide.Location };
            }
        }
        else if (leftSide is ValueExpression<string> leftSideStrValue)
        {
            if (rightSide is ValueExpression<int> rightSideIntValue)
            {
                return new ValueExpression<string> { Value = leftSideStrValue.Value + rightSideIntValue.Value, Location = leftSide.Location };
            }
            else if (rightSide is ValueExpression<string> rightSideStrValue)
            {
                return new ValueExpression<string> { Value = leftSideStrValue.Value + rightSideStrValue.Value, Location = leftSide.Location };
            }
        }
        throw new ArgumentException($"Error on processing near {leftSide.Location.GetLog()}");
    }

    private ValueExpression<int> SubOperation(ValueExpression leftSide, ValueExpression rightSide)
    {
        if (leftSide is ValueExpression<int> leftSideIntValue)
        {
            if (rightSide is ValueExpression<int> rightSideIntValue)
            {
                return new ValueExpression<int> { Value = leftSideIntValue.Value - rightSideIntValue.Value, Location = leftSide.Location };
            }
        }
        throw new ArgumentException($"Error on processing near {leftSide.Location.GetLog()}");
    }

    private ValueExpression<int> MultiOperation(ValueExpression leftSide, ValueExpression rightSide)
    {
        if (leftSide is ValueExpression<int> leftSideIntValue)
        {
            if (rightSide is ValueExpression<int> rightSideIntValue)
            {
                return new ValueExpression<int> { Value = leftSideIntValue.Value * rightSideIntValue.Value, Location = leftSide.Location };
            }
        }
        throw new ArgumentException($"Error on processing near {leftSide.Location.GetLog()}");
    }

    private ValueExpression<int> DivOperation(ValueExpression leftSide, ValueExpression rightSide)
    {
        if (leftSide is ValueExpression<int> leftSideIntValue)
        {
            if (rightSide is ValueExpression<int> rightSideIntValue)
            {
                return new ValueExpression<int> { Value = leftSideIntValue.Value / rightSideIntValue.Value, Location = leftSide.Location };
            }
        }
        throw new ArgumentException($"Error on processing near {leftSide.Location.GetLog()}");
    }

    private ValueExpression<int> RemOperation(ValueExpression leftSide, ValueExpression rightSide)
    {
        if (leftSide is ValueExpression<int> leftSideIntValue)
        {
            if (rightSide is ValueExpression<int> rightSideIntValue)
            {
                return new ValueExpression<int> { Value = leftSideIntValue.Value % rightSideIntValue.Value, Location = leftSide.Location };
            }
        }
        throw new ArgumentException($"Error on processing near {leftSide.Location.GetLog()}");
    }

    private ValueExpression<bool> EqualOperation(ValueExpression leftSide, ValueExpression rightSide)
    {
        if (leftSide is ValueExpression<int> leftSideIntValue)
        {
            if (rightSide is ValueExpression<int> rightSideIntValue)
            {
                return new ValueExpression<bool> { Value = leftSideIntValue.Value == rightSideIntValue.Value, Location = leftSide.Location };
            }
        }
        else if (leftSide is ValueExpression<string> leftSideStrValue)
        {
            if (rightSide is ValueExpression<string> rightSideStrValue)
            {
                return new ValueExpression<bool> { Value = leftSideStrValue.Value == rightSideStrValue.Value, Location = leftSide.Location };
            }
        }
        throw new ArgumentException($"Error on processing near {leftSide.Location.GetLog()}");
    }

    private ValueExpression<bool> NotEqualOperation(ValueExpression leftSide, ValueExpression rightSide)
    {
        if (leftSide is ValueExpression<int> leftSideIntValue)
        {
            if (rightSide is ValueExpression<int> rightSideIntValue)
            {
                return new ValueExpression<bool> { Value = leftSideIntValue.Value != rightSideIntValue.Value, Location = leftSide.Location };
            }
        }
        else if (leftSide is ValueExpression<string> leftSideStrValue)
        {
            if (rightSide is ValueExpression<string> rightSideStrValue)
            {
                return new ValueExpression<bool> { Value = leftSideStrValue.Value != rightSideStrValue.Value, Location = leftSide.Location };
            }
        }
        throw new ArgumentException($"Error on processing near {leftSide.Location.GetLog()}");
    }

    private ValueExpression<bool> LessThanOperation(ValueExpression leftSide, ValueExpression rightSide)
    {
        if (leftSide is ValueExpression<int> leftSideIntValue)
        {
            if (rightSide is ValueExpression<int> rightSideIntValue)
            {
                return new ValueExpression<bool> { Value = leftSideIntValue.Value < rightSideIntValue.Value, Location = leftSide.Location };
            }
        }
        throw new ArgumentException($"Error on processing near {leftSide.Location.GetLog()}");
    }

    private ValueExpression<bool> LessOrEqualThanOperation(ValueExpression leftSide, ValueExpression rightSide)
    {
        if (leftSide is ValueExpression<int> leftSideIntValue)
        {
            if (rightSide is ValueExpression<int> rightSideIntValue)
            {
                return new ValueExpression<bool> { Value = leftSideIntValue.Value <= rightSideIntValue.Value, Location = leftSide.Location };
            }
        }
        throw new ArgumentException($"Error on processing near {leftSide.Location.GetLog()}");
    }

    private ValueExpression<bool> GreaterThanOperation(ValueExpression leftSide, ValueExpression rightSide)
    {
        if (leftSide is ValueExpression<int> leftSideIntValue)
        {
            if (rightSide is ValueExpression<int> rightSideIntValue)
            {
                return new ValueExpression<bool> { Value = leftSideIntValue.Value < rightSideIntValue.Value, Location = leftSide.Location };
            }
        }
        throw new ArgumentException($"Error on processing near {leftSide.Location.GetLog()}");
    }

    private ValueExpression<bool> GreaterOrEqualThanOperation(ValueExpression leftSide, ValueExpression rightSide)
    {
        if (leftSide is ValueExpression<int> leftSideIntValue)
        {
            if (rightSide is ValueExpression<int> rightSideIntValue)
            {
                return new ValueExpression<bool> { Value = leftSideIntValue.Value <= rightSideIntValue.Value, Location = leftSide.Location };
            }
        }
        throw new ArgumentException($"Error on processing near {leftSide.Location.GetLog()}");
    }

    private ValueExpression<bool> AndOperation(ValueExpression leftSide, ValueExpression rightSide)
    {
        if (leftSide is ValueExpression<bool> leftSideIntValue)
        {
            if (rightSide is ValueExpression<bool> rightSideIntValue)
            {
                return new ValueExpression<bool> { Value = leftSideIntValue.Value && rightSideIntValue.Value, Location = leftSide.Location };
            }
        }
        throw new ArgumentException($"Error on processing near {leftSide.Location.GetLog()}");
    }

    private ValueExpression<bool> OrOperation(ValueExpression leftSide, ValueExpression rightSide)
    {
        if (leftSide is ValueExpression<bool> leftSideIntValue)
        {
            if (rightSide is ValueExpression<bool> rightSideIntValue)
            {
                return new ValueExpression<bool> { Value = leftSideIntValue.Value && rightSideIntValue.Value, Location = leftSide.Location };
            }
        }
        throw new ArgumentException($"Error on processing near {leftSide.Location.GetLog()}");
    }
}

public sealed class CallExpression : Expression
{
    public Expression Callee { get; set; }

    public Expression[] Arguments { get; set; }

    public override object Run()
    {
        return Callee.Run();
    }
}


public class RunUntil
{
    public static TExpression Find<TExpression>(Expression expression)
    {
        if (expression is TExpression tExpression) return tExpression;
        var expResult = expression.Run();
        if (expResult is Expression exp) return Find<TExpression>(exp);
        throw new ArgumentException($"Error on processing {expression.Location.GetLog()}");
    }
}
