using Rinha.Interpreter;

namespace Rinha.Test.Interpreter;

public class ConditionExpressionTest
{


    [Fact]
    public void IfTrueThenEq1()
    {
        var binary = new BinaryExpression
        {
            LeftHandSide = new ValueExpression<bool>
            {
                Value = true
            },
            RightHandSide = new ValueExpression<bool>
            {
                Value = true
            },
            Operation = BinaryOperator.And
        };

        var condition = new ConditionExpression
        {
            Condition = binary,
            Then = new ValueExpression<int> { Value = 1 },
            Otherwise = new ValueExpression<int> { Value = 3 }
        };

        var sut = (int)condition.Run();
        sut.Should().Be(1);
    }

    [Fact]
    public void IfFalseThenEq3()
    {
        var binary = new BinaryExpression
        {
            LeftHandSide = new ValueExpression<bool>
            {
                Value = false
            },
            RightHandSide = new ValueExpression<bool>
            {
                Value = true
            },
            Operation = BinaryOperator.And
        };

        var condition = new ConditionExpression
        {
            Condition = binary,
            Then = new ValueExpression<int> { Value = 1 },
            Otherwise = new ValueExpression<int> { Value = 3 }
        };

        var sut = (int)condition.Run();
        sut.Should().Be(3);
    }
}
