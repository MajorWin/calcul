namespace Calcul.Expression.BinaryExpression
{
    public class AdditionExpression : BinaryExpression
    {
        public AdditionExpression(IExpression firstOp, IExpression secondOp) : base(firstOp, secondOp) { }

        protected override int Operation(IExpression firstOp, IExpression secondOp) =>
            firstOp.Calculate() + secondOp.Calculate();
    }
}