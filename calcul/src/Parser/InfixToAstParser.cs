using System.Collections.Generic;
using Calcul.Exceptions;
using Calcul.Exceptions.Expression;
using Calcul.Lexer;
using Calcul.Lexer.Tokens;
using Calcul.Lexer.Tokens.Extensions;
using Calcul.Lexer.Tokens.SymbolTokens.Brackets;
using Calcul.Lexer.Tokens.SymbolTokens.Operations;
using Calcul.Lexer.Tokens.ValueTokens;
using Calcul.Parser.Expressions;
using Calcul.Parser.Expressions.BinaryExpressions;
using Calcul.Parser.Expressions.UnaryExpressions;

namespace Calcul.Parser;

public class InfixToAstParser
{
    private readonly ILexer myLexer;
    private readonly IContext myContext;

    public InfixToAstParser(ILexer lexer)
    {
        myLexer = lexer;
        myContext = new Context();
    }

    public InfixToAstParser(ILexer lexer, IContext context) : this(lexer)
    {
        myLexer = lexer;
        myContext = context;
    }

    public IEnumerable<IExpression> ParseStatements()
    {
        myLexer.GetNext();
        while (myLexer.Current is not EofToken)
        {
            var expr = ParseStatement();
            if (expr is not null) yield return expr;
        }
    }

    public IExpression ParseStatement()
    {
        //TODO:
        if (myLexer.Current is BofToken)
        {
            myLexer.GetCurrentAndMoveNext();
        }

        if (myLexer.Current is EndOfStatementToken)
        {
            return null;
        }

        var res = ParseExpression();
        if (myLexer.Current is EndOfStatementToken)
        {
            myLexer.GetCurrentAndMoveNext();
            return res;
        }

        throw new ParserException(typeof(EndOfStatementToken), myLexer.Current);
    }

    private IExpression ParseExpression() => ParseAdditive();

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
        var left = ParseUnary();

        if (myLexer.Current.IsNot<PowerToken>())
        {
            return left;
        }

        myLexer.GetNext();
        return new PowerExpression(left, ParsePower());
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
            ? new UnaryPlusMinusExpression(plusCount, minusCount, ParseFactorial())
            : ParseFactorial();
    }

    private IExpression ParseFactorial()
    {
        var operand = ParseParentheses();

        return myLexer.Current.Is<ExclamationToken>()
            ? ParseFactorialInternal(operand)
            : operand;

        IExpression ParseFactorialInternal(IExpression expression)
        {
            myLexer.GetNext();
            return myLexer.Current.Is<ExclamationToken>()
                ? new FactorialExpression(ParseFactorialInternal(expression))
                : new FactorialExpression(expression);
        }
    }

    private IExpression ParseParentheses()
    {
        return myLexer.Current switch
        {
            OpenParenthesisToken _ => ParseParenthesesExpression(),
            IntToken _ => ParseNumber(),
            IdentifierToken => ParseVariable(),
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
        var value = ((IntToken)myLexer.GetCurrentAndMoveNext()).Value;
        return new IntNumberExpression(value);
    }

    private IExpression ParseVariable()
    {
        var name = ((IdentifierToken)myLexer.GetCurrentAndMoveNext()).Value;
        if (!myContext.Variables.ContainsKey(name))
        {
            throw new UnknownVariableException(name);
        }

        var value = myContext.Variables[name];
        return new VariableExpression(name, value);
    }
}
