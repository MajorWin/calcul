namespace Calcul.Expression.BinaryExpression;

public class MultiplicationExpression : BinaryExpression
{
    public MultiplicationExpression(IExpression left, IExpression right) : base(left, right) { }
    public override int Calculate() => Left.Calculate() * Right.Calculate();
    public override string ToString() => $"({Left}) * ({Right})";
}