using Calcul.Exceptions.Expression;

namespace Calcul.Expression.BinaryExpression
{
    public class DivisionExpression : BinaryExpression
    {
        public DivisionExpression(IExpression left, IExpression right) : base(left, right) { }
        
        public override int Calculate()
        {
            var right = Right.Calculate();
            if (right == 0)
            {
                throw new DivisionByZeroException(Left, Right);
            }

            return Left.Calculate() / right;
        }

        public override string ToString() => $"({Left}) / ({Right})";
    }
}