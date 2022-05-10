using System;
using System.Collections.Generic;
using Calcul.Exceptions;
using Calcul.Lexer.Tokens;
using Calcul.Lexer.Tokens.Extensions;
using Calcul.Lexer.Tokens.KeywordTokens;
using Calcul.Lexer.Tokens.SymbolTokens.Brackets;
using Calcul.Lexer.Tokens.SymbolTokens.Operations;
using Calcul.Lexer.Tokens.ValueTokens;

namespace Calcul.Lexer;

public class ArithmeticLexer : ILexer
{
    private readonly string myString;
    private int myIndex;
    private readonly IEnumerator<Token> myTokens;

    public ArithmeticLexer(string s)
    {
        myString = s + '\0';
        Current = BofToken.Instance;
        myTokens = ReadProgram().GetEnumerator();
    }


    public Token Current { get; private set; }

    public Token GetNext()
    {
        var hasNext = myTokens.MoveNext();
        if (!hasNext && myIndex != myString.Length - 1)
        {
            throw new LexerException(myString[myIndex].ToString(), myIndex);
        }

        Current = hasNext ? myTokens.Current : new EofToken(myIndex);
        return Current;
    }

    public Token GetCurrentAndMoveNext()
    {
        var oldCurrent = Current;
        GetNext();
        return oldCurrent;
    }


    private IEnumerable<Token> ReadProgram()
    {
        while (Current is not EofToken)
        {
            foreach (var token in ReadSentence())
            {
                yield return token;
            }

            yield return ReadEndOfStatementToken();
        }
    }

    private IEnumerable<Token> ReadSentence()
    {
        SkipSpaces();
        if (!myString[myIndex].IsStartOfIdentifier())
        {
            foreach (var additiveToken in ReadAdditive())
            {
                yield return additiveToken;
            }

            yield break;
        }

        var index = myIndex;
        var token = ReadIdentifier();
        IEnumerable<Token> sentence;
        if (token is VarToken)
        {
            ReadIdentifier();
            SkipSpaces();
            if (TryReadEndOfStatementToken() is null)
            {
                myIndex = index;
                sentence = ReadSetVariable();
            }
            else
            {
                myIndex = index;
                sentence = ReadExpression();
            }
        }
        else
        {
            myIndex = index;
            sentence = ReadExpression();
        }

        foreach (var sentenceToken in sentence)
        {
            yield return sentenceToken;
        }
    }

    private IEnumerable<Token> ReadExpression()
    {
        SkipSpaces();
        if (!myString[myIndex].IsStartOfIdentifier())
        {
            foreach (var token in ReadAdditive())
            {
                yield return token;
            }

            yield break;
        }

        var index = myIndex;
        var next = ReadIdentifier();

        IEnumerable<Token> result;

        switch (next)
        {
            case VarToken:
                myIndex = index;
                result = ReadDefineAndSetVariable();
                break;
            case IdentifierToken:
            {
                SkipSpaces();
                if (myString[myIndex] == '=')
                {
                    myIndex = index;
                    result = ReadSetVariable();
                }
                else
                {
                    myIndex = index;
                    result = ReadAdditive();
                }
                break;
            }
            default:
                throw new LexerException(next.ToString(), index);
        }

        foreach (var token in result)
        {
            yield return token;
        }
    }

    private IEnumerable<Token> ReadNonArithmeticExpression()
    {
        SkipSpaces();
        var index = myIndex;
        var next = ReadIdentifier();

        IEnumerable<Token> result;

        switch (next)
        {
            case VarToken:
                myIndex = index;
                result = ReadDefineAndSetVariable();
                break;
            case IdentifierToken:
            {
                myIndex = index;
                result = ReadSetVariable();
                break;
            }
            default:
                throw new LexerException(next.ToString(), index);
        }

        foreach (var token in result)
        {
            yield return token;
        }
    }

    private IEnumerable<Token> ReadDefineVariable()
    {
        SkipSpaces();
        var token = ReadIdentifier();
        if (token is not VarToken)
        {
            throw new LexerException("var", token.ToString(), token.Offset);
        }

        SkipSpaces();
        token = ReadIdentifier();
        if (token is not IdentifierToken)
        {
            throw new LexerException("identifier", token.ToString(), token.Offset);
        }

        yield return token;
    }

    private IEnumerable<Token> ReadDefineAndSetVariable()
    {
        foreach (var token in ReadDefineVariable())
        {
            yield return token;
        }

        SkipSpaces();
        if (myString[myIndex] != '=')
        {
            throw new LexerException("=", myString[myIndex].ToString(), myIndex);
        }

        yield return new EqualsToken(myIndex);

        myIndex++;
        foreach (var token in ReadExpression())
        {
            yield return token;
        }
    }

    private IEnumerable<Token>
        ReadSetVariable()
    {
        var variable = ReadIdentifier();
        if (variable is not IdentifierToken)
        {
            throw new LexerException("variable name", variable.ToString(), variable.Offset);
        }

        yield return variable;

        SkipSpaces();
        if (myString[myIndex] != '=')
        {
            throw new LexerException("=", myString[myIndex].ToString(), myIndex);
        }

        yield return new EqualsToken(myIndex);

        myIndex++;
        foreach (var token in ReadExpression())
        {
            yield return token;
        }
    }

    private IEnumerable<Token> ReadAdditive()
    {
        while (true)
        {
            foreach (var token in ReadMultiplicative())
            {
                yield return token;
            }

            SkipSpaces();
            var nextChar = myString[myIndex];
            if (nextChar != '+' && nextChar != '-') yield break;

            myIndex++;
            yield return nextChar switch
            {
                '+' => new PlusToken(myIndex - 1),
                _ => new MinusToken(myIndex - 1)
            };
        }
    }

    private IEnumerable<Token> ReadMultiplicative()
    {
        while (true)
        {
            foreach (var token in ReadPower())
            {
                yield return token;
            }

            SkipSpaces();
            var nextChar = myString[myIndex];
            if (nextChar != '*' && nextChar != '/') yield break;

            myIndex++;
            yield return nextChar switch
            {
                '*' => new MultiplyToken(myIndex - 1),
                _ => new DivideToken(myIndex - 1)
            };
        }
    }

    private IEnumerable<Token> ReadPower()
    {
        while (true)
        {
            foreach (var token in ReadUnary())
            {
                yield return token;
            }

            SkipSpaces();
            if (myString[myIndex] != '*' || myString[myIndex + 1] != '*') yield break;

            myIndex += 2;
            yield return new PowerToken(myIndex - 2);
        }
    }

    private IEnumerable<Token> ReadUnary()
    {
        SkipSpaces();
        while (myString[myIndex] == '+' || myString[myIndex] == '-')
        {
            var tokenIndex = myIndex;
            myIndex++;
            yield return myString[tokenIndex] == '+'
                ? new PlusToken(tokenIndex)
                : new MinusToken(tokenIndex);
            SkipSpaces();
        }

        foreach (var token in ReadFactorial())
        {
            yield return token;
        }
    }

    private IEnumerable<Token> ReadFactorial()
    {
        foreach (var token in ReadParentheses())
        {
            yield return token;
        }

        SkipSpaces();
        while (myString[myIndex] == '!')
        {
            myIndex++;
            yield return new ExclamationToken(myIndex - 1);
            SkipSpaces();
        }
    }

    private IEnumerable<Token> ReadParentheses()
    {
        SkipSpaces();
        if (myString[myIndex] != '(')
        {
            if (char.IsNumber(myString[myIndex]))
            {
                yield return ReadNumber();
                yield break;
            }

            if (myString[myIndex].IsStartOfIdentifier())
            {
                yield return ReadVariable();
                yield break;
            }

            foreach (var token in ReadNonArithmeticExpression())
            {
                yield return token;
            }

            yield break;
        }

        // (
        myIndex++;
        yield return new OpenParenthesisToken(myIndex - 1);

        // number
        foreach (var token in ReadAdditive())
        {
            yield return token;
        }

        // )
        SkipSpaces();
        if (myString[myIndex] == ')')
        {
            myIndex++;
            yield return new CloseParenthesisToken(myIndex - 1);
        }
        else
        {
            throw new Exception($"Expected ')', found: '{myString[myIndex]}' on position {myIndex}");
        }
    }

    private IntToken ReadNumber()
    {
        SkipSpaces();
        if (!char.IsDigit(myString[myIndex]))
        {
            throw new LexerException("[0-9]", myString[myIndex].ToString(), myIndex);
        }

        var nextToLast = myIndex + 1;
        while (char.IsDigit(myString[nextToLast]))
        {
            nextToLast++;
        }

        var length = nextToLast - myIndex;
        var number = int.Parse(myString.Substring(myIndex, length));

        var result = new IntToken(myIndex, number);
        myIndex = nextToLast;

        return result;
    }

    private IdentifierToken ReadVariable()
    {
        SkipSpaces();
        var token = ReadIdentifier();
        if (token is not IdentifierToken identifier)
        {
            throw new LexerException("variable name", $"token", token.Offset);
        }

        return identifier;
    }

    private Token ReadIdentifier()
    {
        SkipSpaces();
        if (!myString[myIndex].IsStartOfIdentifier())
        {
            throw new LexerException("identifier", myString[myIndex].ToString(), myIndex);
        }

        var nextToLast = myIndex + 1;
        while (myString[nextToLast].IsIdentifierBody())
        {
            nextToLast++;
        }

        var length = nextToLast - myIndex;
        var identifier = myString.Substring(myIndex, length);

        Token result = identifier switch
        {
            Keywords.Var => new VarToken(myIndex),
            _ => new IdentifierToken(myIndex, identifier)
        };
        myIndex = nextToLast;

        return result;
    }

    private EndOfStatementToken TryReadEndOfStatementToken()
    {
        return myString[myIndex] switch
        {
            '\r' => ReadRn(),
            '\n' => ReadN(),
            '\0' => new EofToken(myIndex),
            _ => null
        };

        EndOfStatementToken ReadRn()
        {
            if (myString[myIndex + 1] != '\n')
            {
                throw new LexerException(@"\r\n", $"\"\\r{myString[myIndex + 1]}\"", myIndex);
            }

            myIndex += 2;
            return new EndOfStatementToken(myIndex - 2, "\r\n");
        }

        EndOfStatementToken ReadN()
        {
            myIndex += 1;
            return new EndOfStatementToken(myIndex - 1, "\n");
        }
    }

    private EndOfStatementToken ReadEndOfStatementToken()
    {
        var result = TryReadEndOfStatementToken();
        if (result is null)
        {
            throw new LexerException("End of statement or end of program", myIndex);
        }

        return result;
    }

    private void SkipSpaces()
    {
        while (char.IsWhiteSpace(myString[myIndex]) && myString[myIndex] != '\r' && myString[myIndex] != '\n')
        {
            myIndex++;
        }
    }
}