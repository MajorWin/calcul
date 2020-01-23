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
            var left = ParseTerm();
            while (myLexer.Current.IsExprToken())
            {
                var operation = myLexer.GetCurrentAndMoveNext();
                var right = ParseTerm();
                left = operation switch
                {
                    PlusToken _ => new AdditionExpression(left, right),
                    MinusToken _ => new SubtractionExpression(left, right),
                    // TODO: add new exception type
                    _ => throw new Exception($"Invalid token: {left}")
                };
            }

            return left;
        }

        private IExpression ParseTerm()
        {
            var left = ParseFactory();
            while (myLexer.Current.IsTermToken())
            {
                var operation = myLexer.GetCurrentAndMoveNext();
                var right = ParseFactory();
                left = operation switch
                {
                    MultiplyToken _ => new MultiplicationExpression(left, right),
                    DivideToken _ => new DivisionExpression(left, right),
                    // TODO: add new exception type
                    _ => throw new Exception($"Invalid token: {left}")
                };
            }

            return left;
        }

        private IExpression ParseFactory()
        {
            return myLexer.Current switch
            {
                IntToken _ => ParseNumber(),
                OpenParenthesisToken _ => ParseParenthesesExpression(),
                _ => throw new Exception($"!(token is IntToken): {myLexer.Current}")
            };
        }

        private IExpression ParseNumber()
        {
            var value = ((IntToken) myLexer.GetCurrentAndMoveNext()).Value;
            return new IntNumberExpression(value);
        }

        private IExpression ParseParenthesesExpression()
        {
            // (
            myLexer.GetNext();
            
            var expr = ParseExpr();
            
            // )
            var afterExprToken = myLexer.Current;
            if (afterExprToken.IsNot<CloseParenthesisToken>())
                throw new Exception($"Invalid bracket expression: found {afterExprToken}");
            myLexer.GetNext();

            return expr;
        }
    }
}