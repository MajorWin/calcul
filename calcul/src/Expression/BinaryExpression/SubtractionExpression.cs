namespace Calcul.Expression.BinaryExpression
{
    public class SubtractionExpression : BinaryExpression
    {
        public SubtractionExpression(IExpression left, IExpression right) : base(left, right) { }
        public override int Calculate() => Left.Calculate() - Right.Calculate();
        public override string ToString() => $"({Left}) - ({Right})";
    }
}