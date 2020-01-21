namespace Calcul.Expression.BinaryExpression
{
    public class DivisionExpression : BinaryExpression
    {
        public DivisionExpression(IExpression firstOp, IExpression secondOp) : base(firstOp, secondOp) { }

        protected override int Operation(IExpression firstOp, IExpression secondOp) =>
            firstOp.Calculate() / secondOp.Calculate();
    }
}