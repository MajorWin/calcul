using System;
using System.Collections.Generic;
using System.Linq;
using Calcul.Tokens;
using Calcul.Tokens.ValueToken.OperationToken;

namespace Calcul
{
    public class IntExpressionInterpreter
    {
        private readonly IReadOnlyList<IToken> myExpression;

        private readonly Dictionary<OperationToken, IBinaryOperation> myBinaryOperations =
            new Dictionary<OperationToken, IBinaryOperation>
            {
                {PlusToken.Instance, PlusOperation.Instance},
                {MinusToken.Instance, MinusOperation.Instance},
                {MultiplyToken.Instance, MultiplyOperation.Instance},
                {DivideToken.Instance, DivideOperation.Instance}
            };

        public IntExpressionInterpreter(IReadOnlyList<IToken> expression) => myExpression = expression;

        public int Interpret()
        {
            var result = new List<int>();
            foreach (var token in myExpression.Reverse())
            {
                switch (token)
                {
                    case IntToken it:
                        result.Add(it.Value);
                        break;
                    case OperationToken ot:
                        var size = result.Count;
                        result[size - 2] = myBinaryOperations[ot].Calculate(
                            result[size - 1],
                            result[size - 2]
                        );
                        result.RemoveAt(size - 1);
                        break;
                    default:
                        throw new Exception("what the keck");
                }
            }

            return result[0];
        }
    }

    internal interface IOperation { }

    internal interface IBinaryOperation : IOperation
    {
        public int Calculate(int left, int right);
    }

    internal class PlusOperation : IBinaryOperation
    {
        public static readonly PlusOperation Instance = new PlusOperation();
        public int Calculate(int left, int right) => left + right;
    }

    internal class MinusOperation : IBinaryOperation
    {
        public static readonly MinusOperation Instance = new MinusOperation();
        public int Calculate(int left, int right) => left - right;
    }

    internal class MultiplyOperation : IBinaryOperation
    {
        public static readonly MultiplyOperation Instance = new MultiplyOperation();
        public int Calculate(int left, int right) => left * right;
    }

    internal class DivideOperation : IBinaryOperation
    {
        public static readonly DivideOperation Instance = new DivideOperation();
        public int Calculate(int left, int right) => left / right;
    }
}