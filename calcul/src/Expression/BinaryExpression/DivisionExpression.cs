namespace Calcul.Expression.BinaryExpression
{
    public class DivisionExpression : BinaryExpression
    {
        public DivisionExpression(IExpression left, IExpression right) : base(left, right) { }
        public override int Calculate() => Left.Calculate() / Right.Calculate();
    }
}