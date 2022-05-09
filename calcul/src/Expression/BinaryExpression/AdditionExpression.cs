namespace Calcul.Expression.BinaryExpression;

public record AdditionExpression(IExpression Left, IExpression Right) : BinaryExpression(Left, Right)
{
    public override int Calculate() => Left.Calculate() + Right.Calculate();
    public override string ToString() => $"({Left}) + ({Right})";
}