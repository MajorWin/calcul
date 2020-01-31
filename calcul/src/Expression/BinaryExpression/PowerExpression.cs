using System;

namespace Calcul.Expression.BinaryExpression
{
    public class PowerExpression : BinaryExpression
    {
        public PowerExpression(IExpression left, IExpression right) : base(left, right) { }
        public override int Calculate() => Convert.ToInt32(Math.Pow(Left.Calculate(), Right.Calculate()));
        public override string ToString() => $"({Left}) ** ({Right})";
    }
}