using System;

namespace Calcul.Expression
{
    public class IntNumberExpression : IExpression
    {
        private readonly int myValue;

        public IntNumberExpression(int value) => myValue = value;

        public int Calculate() => myValue;
    }
}