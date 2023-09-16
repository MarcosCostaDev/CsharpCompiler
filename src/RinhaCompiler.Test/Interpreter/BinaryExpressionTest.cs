using Rinha.Interpreter;

namespace Rinha.Test.Interpreter;

public class BinaryExpressionTest
{

    [Fact]
    public void Add_Result_Eq_3()
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
    public void AddConcat_Result_Eq_12()
    {
        var binary = new BinaryExpression
        {
            LeftHandSide = new ValueExpression<int>
            {
                Value = 1
            },
            RightHandSide = new ValueExpression<string>
            {
                Value = "2"
            },
            Operation = BinaryOperator.Add
        };

        var sut = binary.Run() as ValueExpression<string>;
        sut.Value.Should().Be("12");
    }

    [Fact]
    public void AddConcat_Result_Eq_32()
    {
        var binary = new BinaryExpression
        {
            LeftHandSide = new ValueExpression<string>
            {
                Value = "3"
            },
            RightHandSide = new ValueExpression<int>
            {
                Value = 2
            },
            Operation = BinaryOperator.Add
        };

        var sut = binary.Run() as ValueExpression<string>;
        sut.Value.Should().Be("32");
    }

    [Fact]
    public void SubResult_Eq_5()
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
    public void SubResult_Eq_Minus5()
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
    public void MulResult_Eq_15()
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
    public void DivResult_Eq_5()
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
    public void RemResult_Eq_5()
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
    public void EqIntResult_Eq_True()
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
    public void EqIntResult_Eq_False()
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
    public void EqIntResult_Neq_False()
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
    public void EqIntResult_Neq_True()
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
    public void EqBoolResult_Eq_True()
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
    public void EqBoolResult_Eq_False()
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
    public void EqBoolResult_Neq_False()
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
    public void EqBoolResult_NeqTrue()
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
    public void AndBoolResult_Eq_True()
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
    public void AndBoolResult_Eq_False()
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
    public void OrBoolResult_Eq_True()
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
    public void OrBoolResult_Eq_False()
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
