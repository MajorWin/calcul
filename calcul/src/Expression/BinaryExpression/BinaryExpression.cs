namespace Calcul.Expression.BinaryExpression
{
    public abstract class BinaryExpression : IExpression
    {
        private readonly IExpression myFirstOp;
        private readonly IExpression mySecondOp;

        protected BinaryExpression(IExpression firstOp, IExpression secondOp)
        {
            myFirstOp = firstOp;
            mySecondOp = secondOp;
        }
        
        protected abstract int Operation(IExpression firstOp, IExpression secondOp);
        
        public int Calculate() => Operation(myFirstOp, mySecondOp);
    }
}