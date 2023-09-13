using RinhaCompiler.Interpreter;

namespace RinhaCompiler.Test.Interpreter;

public class BinaryExpressionTest
{

    [Fact]
    public void AddResultEq3()
    {
        var binary = new BinaryExpression
        {
            LeftHandSide = new ValueExpression<int>
            {
                Value = 1
            },
            RightHandSide = new ValueExpression<int>
            {
                Value = 2
            },
            Operation = BinaryOperator.Add
        };

        var sut = binary.Run() as ValueExpression<int>;
        sut.Value.Should().Be(3);
    }

    [Fact]
    public void SubResultEq5()
    {
        var binary = new BinaryExpression
        {
            LeftHandSide = new ValueExpression<int>
            {
                Value = 8
            },
            RightHandSide = new ValueExpression<int>
            {
                Value = 3
            },
            Operation = BinaryOperator.Sub
        };

        var sut = binary.Run() as ValueExpression<int>;
        sut.Value.Should().Be(5);
    }

    [Fact]
    public void SubResultEqMinus5()
    {
        var binary = new BinaryExpression
        {
            LeftHandSide = new ValueExpression<int>
            {
                Value = 3
            },
            RightHandSide = new ValueExpression<int>
            {
                Value = 8
            },
            Operation = BinaryOperator.Sub
        };

        var sut = binary.Run() as ValueExpression<int>;
        sut.Value.Should().Be(-5);
    }

    [Fact]
    public void MulResultEq15()
    {
        var binary = new BinaryExpression
        {
            LeftHandSide = new ValueExpression<int>
            {
                Value = 3
            },
            RightHandSide = new ValueExpression<int>
            {
                Value = 5
            },
            Operation = BinaryOperator.Mul
        };

        var sut = binary.Run() as ValueExpression<int>;
        sut.Value.Should().Be(15);
    }

    [Fact]
    public void DivResultEq5()
    {
        var binary = new BinaryExpression
        {
            LeftHandSide = new ValueExpression<int>
            {
                Value = 15
            },
            RightHandSide = new ValueExpression<int>
            {
                Value = 3
            },
            Operation = BinaryOperator.Div
        };

        var sut = binary.Run() as ValueExpression<int>;
        sut.Value.Should().Be(5);
    }

    [Fact]
    public void RemResultEq5()
    {
        var binary = new BinaryExpression
        {
            LeftHandSide = new ValueExpression<int>
            {
                Value = 15
            },
            RightHandSide = new ValueExpression<int>
            {
                Value = 4
            },
            Operation = BinaryOperator.Rem
        };

        var sut = binary.Run() as ValueExpression<int>;
        sut.Value.Should().Be(3);
    }


    [Fact]
    public void EqIntResultEqTrue()
    {
        var binary = new BinaryExpression
        {
            LeftHandSide = new ValueExpression<int>
            {
                Value = 10
            },
            RightHandSide = new ValueExpression<int>
            {
                Value = 10
            },
            Operation = BinaryOperator.Eq
        };

        var sut = binary.Run() as ValueExpression<bool>;
        sut.Value.Should().BeTrue();
    }


    [Fact]
    public void EqIntResultEqFalse()
    {
        var binary = new BinaryExpression
        {
            LeftHandSide = new ValueExpression<int>
            {
                Value = 10
            },
            RightHandSide = new ValueExpression<int>
            {
                Value = 11
            },
            Operation = BinaryOperator.Eq
        };

        var sut = binary.Run() as ValueExpression<bool>;
        sut.Value.Should().BeFalse();
    }

    [Fact]
    public void EqIntResultNeqFalse()
    {
        var binary = new BinaryExpression
        {
            LeftHandSide = new ValueExpression<int>
            {
                Value = 10
            },
            RightHandSide = new ValueExpression<int>
            {
                Value = 10
            },
            Operation = BinaryOperator.Neq
        };

        var sut = binary.Run() as ValueExpression<bool>;
        sut.Value.Should().BeFalse();
    }


    [Fact]
    public void EqIntResultNeqTrue()
    {
        var binary = new BinaryExpression
        {
            LeftHandSide = new ValueExpression<int>
            {
                Value = 10
            },
            RightHandSide = new ValueExpression<int>
            {
                Value = 11
            },
            Operation = BinaryOperator.Neq
        };

        var sut = binary.Run() as ValueExpression<bool>;
        sut.Value.Should().BeTrue();
    }


    [Fact]
    public void EqBoolResultEqTrue()
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
            Operation = BinaryOperator.Eq
        };

        var sut = binary.Run() as ValueExpression<bool>;
        sut.Value.Should().BeTrue();
    }


    [Fact]
    public void EqBoolResultEqFalse()
    {
        var binary = new BinaryExpression
        {
            LeftHandSide = new ValueExpression<bool>
            {
                Value = true
            },
            RightHandSide = new ValueExpression<bool>
            {
                Value = false
            },
            Operation = BinaryOperator.Eq
        };

        var sut = binary.Run() as ValueExpression<bool>;
        sut.Value.Should().BeFalse();
    }

    [Fact]
    public void EqBoolResultNeqFalse()
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
            Operation = BinaryOperator.Neq
        };

        var sut = binary.Run() as ValueExpression<bool>;
        sut.Value.Should().BeFalse();
    }


    [Fact]
    public void EqBoolResultNeqTrue()
    {
        var binary = new BinaryExpression
        {
            LeftHandSide = new ValueExpression<bool>
            {
                Value = true
            },
            RightHandSide = new ValueExpression<bool>
            {
                Value = false
            },
            Operation = BinaryOperator.Neq
        };

        var sut = binary.Run() as ValueExpression<bool>;
        sut.Value.Should().BeTrue();
    }


    [Fact]
    public void AndBoolResultEqTrue()
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

        var sut = binary.Run() as ValueExpression<bool>;
        sut.Value.Should().BeTrue();
    }

    [Fact]
    public void AndBoolResultEqFalse()
    {
        var binary = new BinaryExpression
        {
            LeftHandSide = new ValueExpression<bool>
            {
                Value = true
            },
            RightHandSide = new ValueExpression<bool>
            {
                Value = false
            },
            Operation = BinaryOperator.And
        };

        var sut = binary.Run() as ValueExpression<bool>;
        sut.Value.Should().BeFalse();
    }

    [Fact]
    public void OrBoolResultEqTrue()
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
            Operation = BinaryOperator.Or
        };

        var sut = binary.Run() as ValueExpression<bool>;
        sut.Value.Should().BeTrue();
    }

    [Fact]
    public void OrBoolResultEqFalse()
    {
        var binary = new BinaryExpression
        {
            LeftHandSide = new ValueExpression<bool>
            {
                Value = false
            },
            RightHandSide = new ValueExpression<bool>
            {
                Value = false
            },
            Operation = BinaryOperator.And
        };

        var sut = binary.Run() as ValueExpression<bool>;
        sut.Value.Should().BeFalse();
    }
}
