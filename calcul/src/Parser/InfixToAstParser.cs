using System;
using Calcul.Exceptions;
using Calcul.Expression;
using Calcul.Expression.BinaryExpression;
using Calcul.Expression.UnaryExpression;
using Calcul.Extensions;
using Calcul.Lexer;
using Calcul.Token;
using Calcul.Token.ValueToken;
using Calcul.Token.ValueToken.Brackets;
using Calcul.Token.ValueToken.Operations;

namespace Calcul.Parser
{
    public class InfixToAstParser : IParser
    {
        private readonly ILexer myLexer;

        public InfixToAstParser(ILexer lexer) => myLexer = lexer;

        public IExpression Parse()
        {
            return myLexer.GetNext() is EofToken ? new IntNumberExpression(0) : ParseAdditive();
        }

        private IExpression ParseAdditive()
        {
            var left = ParseMultiplicative();
            while (myLexer.Current.IsAdditiveToken())
            {
                var operation = myLexer.GetCurrentAndMoveNext();
                var right = ParseMultiplicative();
                left = operation switch
                {
                    PlusToken _ => new AdditionExpression(left, right),
                    _ => new SubtractionExpression(left, right),
                };
            }

            return left;
        }

        private IExpression ParseMultiplicative()
        {
            var left = ParsePower();
            while (myLexer.Current.IsMultiplicativeToken())
            {
                var operation = myLexer.GetCurrentAndMoveNext();
                var right = ParsePower();
                left = operation switch
                {
                    MultiplyToken _ => new MultiplicationExpression(left, right),
                    _ => new DivisionExpression(left, right)
                };
            }

            return left;
        }

        private IExpression ParsePower()
        {
            var left = ParseFactorial();

            if (myLexer.Current.IsNot<PowerToken>())
            {
                return left;
            }
            
            myLexer.GetNext();
            return new PowerExpression(left, ParsePower());
        }

        private IExpression ParseFactorial()
        {
            var operand = ParseUnary();
            
            return myLexer.Current.Is<ExclamationToken>()
                ? ParseFactorial(operand)
                : operand;

            IExpression ParseFactorial(IExpression unaryExpression)
            {
                myLexer.GetNext();
                return myLexer.Current.Is<ExclamationToken>()
                    ? new FactorialExpression(ParseFactorial(unaryExpression))
                    : new FactorialExpression(unaryExpression);
            }
        }

        private IExpression ParseUnary()
        {
            var plusCount = 0;
            var minusCount = 0;
            while (myLexer.Current.IsUnary())
            {
                if (myLexer.GetCurrentAndMoveNext().Is<PlusToken>())
                {
                    plusCount++;
                }
                else
                {
                    minusCount++;
                }
            }

            return plusCount > 0 || minusCount > 0
                ? new UnaryPlusMinusExpression(plusCount, minusCount, ParseParentheses())
                : ParseParentheses();
        }

        private IExpression ParseParentheses()
        {
            return myLexer.Current switch
            {
                OpenParenthesisToken _ => ParseParenthesesExpression(),
                IntToken _ => ParseNumber(),
                _ => throw new ParserException((typeof(IntToken), typeof(OpenParenthesisToken)), myLexer.Current)
            };
        }

        private IExpression ParseParenthesesExpression()
        {
            // (
            myLexer.GetNext();

            var expr = ParseAdditive();

            // )
            var afterExprToken = myLexer.Current;
            if (afterExprToken.IsNot<CloseParenthesisToken>())
            {
                throw new ParserException(typeof(CloseParenthesisToken), afterExprToken);
            }

            myLexer.GetNext();

            return expr;
        }

        private IExpression ParseNumber()
        {
            var value = ((IntToken) myLexer.GetCurrentAndMoveNext()).Value;
            return new IntNumberExpression(value);
        }
    }
}