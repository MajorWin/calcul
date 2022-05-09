using System.Text;

namespace Calcul.Expression.UnaryExpression;

public record UnaryPlusMinusExpression(int PlusCount, int MinusCount, IExpression Expression) : IExpression
{
    private int PlusCount { get; } = PlusCount;
    private int MinusCount { get; } = MinusCount;
    private IExpression Expression { get; } = Expression;

    public int Calculate()
    {
        var multiplier = MinusCount % 2 == 0 ? 1 : -1;
        return Expression.Calculate() * multiplier;
    }

    public override string ToString() =>
        PlusCount > 0 || MinusCount > 0
            ? new StringBuilder()
                .Append('-', MinusCount)
                .Append('+', PlusCount)
                .Append('(')
                .Append(Expression)
                .Append(')')
                .ToString()
            : Expression.ToString();

}