using Calcul.Exceptions.Expression;

namespace Calcul.Expression.BinaryExpression;

public record DivisionExpression(IExpression Left, IExpression Right) : BinaryExpression(Left, Right)
{
    public override int Calculate()
    {
        var right = Right.Calculate();
        if (right == 0)
        {
            throw new DivisionByZeroException(Left, Right);
        }

        return Left.Calculate() / right;
    }

    public override string ToString() => $"({Left}) / ({Right})";
}