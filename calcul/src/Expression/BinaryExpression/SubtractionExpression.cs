namespace Calcul.Expression.BinaryExpression
{
    public class SubtractionExpression : BinaryExpression
    {
        public SubtractionExpression(IExpression firstOp, IExpression secondOp) : base(firstOp, secondOp) { }

        protected override int Operation(IExpression firstOp, IExpression secondOp) =>
            firstOp.Calculate() - secondOp.Calculate();
    }
}