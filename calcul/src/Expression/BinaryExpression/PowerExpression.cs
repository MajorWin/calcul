using System;

namespace Calcul.Expression.BinaryExpression;

public record PowerExpression(IExpression Left, IExpression Right) : BinaryExpression(Left, Right)
{
    public override int Calculate() => Convert.ToInt32(Math.Pow(Left.Calculate(), Right.Calculate()));
    public override string ToString() => $"({Left}) ** ({Right})";
}