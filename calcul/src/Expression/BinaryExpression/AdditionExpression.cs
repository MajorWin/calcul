namespace Calcul.Expression.BinaryExpression;

public class AdditionExpression : BinaryExpression
{
    public AdditionExpression(IExpression left, IExpression right) : base(left, right) { }
    public override int Calculate() => Left.Calculate() + Right.Calculate();
    public override string ToString() => $"({Left}) + ({Right})";
}