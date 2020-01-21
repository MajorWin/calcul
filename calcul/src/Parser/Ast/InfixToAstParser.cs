using System;
using Calcul.Expression;
using Calcul.Expression.BinaryExpression;
using Calcul.Extensions;
using Calcul.Lexer;
using Calcul.Token;
using Calcul.Token.ValueToken;
using Calcul.Token.ValueToken.OperationToken;

namespace Calcul.Parser.Ast
{
    public class InfixToAstParser : IParser
    {
        private readonly ILexer myLexer;

        public InfixToAstParser(ILexer lexer) => myLexer = lexer;

        public IExpression Parse()
        {
            return myLexer.GetNext() is EofToken ? new IntNumberExpression(0) : ParseExpr();
        }

        private IExpression ParseExpr()
        {
            var firstOp = ParseTerm();
            while (myLexer.Current.IsExprToken())
            {
                var operation = myLexer.GetCurrentAndMoveNext();
                var secondOp = ParseTerm();
                firstOp = operation switch
                {
                    PlusToken _ => new AdditionExpression(firstOp, secondOp),
                    MinusToken _ => new SubtractionExpression(firstOp, secondOp),
                    // TODO: add new exception type
                    _ => throw new Exception($"Invalid token: {firstOp}")
                };
            }

            return firstOp;
        }

        private IExpression ParseTerm()
        {
            var firstOp = ParseFactory();
            while (myLexer.Current.IsTermToken())
            {
                var operation = myLexer.GetCurrentAndMoveNext();
                var secondOp = ParseFactory();
                firstOp = operation switch
                {
                    MultiplyToken _ => new MultiplicationExpression(firstOp, secondOp),
                    DivideToken _ => new DivisionExpression(firstOp, secondOp),
                    // TODO: add new exception type
                    _ => throw new Exception($"Invalid token: {firstOp}")
                };
            }

            return firstOp;
        }

        private IExpression ParseFactory()
        {
            var factory = myLexer.GetCurrentAndMoveNext();
            if (!(factory is IntToken))
            {
                throw new Exception($"!(token is IntToken): {factory}");
            }

            return new IntNumberExpression(((IntToken) factory).Value);
        }
    }
}