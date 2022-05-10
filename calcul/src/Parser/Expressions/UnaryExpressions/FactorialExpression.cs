using Calcul.Exceptions.Expression;

namespace Calcul.Parser.Expressions.UnaryExpressions;

public class FactorialExpression : IExpression
{
    private readonly IExpression myOperand;

    public FactorialExpression(IExpression operand)
    {
        myOperand = operand;
    }

    public int Calculate()
    {
        var result = myOperand.Calculate();

        if (result < 0)
        {
            throw new FactorialLessThanZero(myOperand, result);
        }

        if (result == 0)
        {
            return 1;
        }

        for (var multiplier = result - 1; multiplier > 1; multiplier--)
        {
            result *= multiplier;
        }

        return result;
    }

    public override string ToString() => $"({myOperand})!";
}