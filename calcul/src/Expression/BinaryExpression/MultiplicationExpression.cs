namespace Calcul.Expression.BinaryExpression
{
    public class MultiplicationExpression : BinaryExpression
    {
        public MultiplicationExpression(IExpression firstOp, IExpression secondOp) : base(firstOp, secondOp) { }

        protected override int Operation(IExpression firstOp, IExpression secondOp) =>
            firstOp.Calculate() * secondOp.Calculate();
    }
}