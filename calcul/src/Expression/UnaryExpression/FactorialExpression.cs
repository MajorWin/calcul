using Calcul.Exceptions.Expression;

namespace Calcul.Expression.UnaryExpression;

public record FactorialExpression(IExpression Operand) : IExpression
{
    private IExpression Operand { get; } = Operand;

    public int Calculate()
    {
        var result = Operand.Calculate();

        if (result < 0)
        {
            throw new FactorialLessThanZero(Operand, result);
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

    public override string ToString() => $"({Operand})!";
}