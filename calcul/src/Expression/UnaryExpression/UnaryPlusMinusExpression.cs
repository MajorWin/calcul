using System.Text;

namespace Calcul.Expression.UnaryExpression
{
    public class UnaryPlusMinusExpression : IExpression
    {
        private readonly int myPlusCount;
        private readonly int myMinusCount;
        private readonly IExpression myExpression;

        public UnaryPlusMinusExpression(int plusCount, int minusCount, IExpression expression)
        {
            myPlusCount = plusCount;
            myMinusCount = minusCount;
            myExpression = expression;
        }

        public int Calculate()
        {
            var multiplier = myMinusCount % 2 == 0 ? 1 : -1;
            return myExpression.Calculate() * multiplier;
        }

        public override string ToString() =>
            myPlusCount > 0 || myMinusCount > 0
                ? new StringBuilder()
                    .Append('-', myMinusCount)
                    .Append('+', myPlusCount)
                    .Append('(')
                    .Append(myExpression.ToString())
                    .Append(')')
                    .ToString()
                : myExpression.ToString();
    }
}