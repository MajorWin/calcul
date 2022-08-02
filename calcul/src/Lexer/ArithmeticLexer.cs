using System;
using System.Collections.Generic;
using Calcul.Exceptions;
using Calcul.Tokens;
using Calcul.Tokens.ValueToken;
using Calcul.Tokens.ValueToken.Brackets;
using Calcul.Tokens.ValueToken.Operations;

namespace Calcul.Lexer
{
    public class ArithmeticLexer : ILexer
    {
        private readonly string myString;
        private int myIndex;
        private readonly IEnumerator<Token> myTokens;

        public ArithmeticLexer(string s)
        {
            myString = s + '\0';
            Current = BofToken.Instance;
            myTokens = ReadAdditive().GetEnumerator();
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

        private void SkipSpaces()
        {
            while (char.IsWhiteSpace(myString[myIndex]))
            {
                myIndex++;
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
                foreach (var token in ReadFactorial())
                {
                    yield return token;
                }

                SkipSpaces();
                if (myString[myIndex] != '*' || myString[myIndex + 1] != '*') yield break;

                myIndex += 2;
                yield return new PowerToken(myIndex - 2);
            }
        }

        private IEnumerable<Token> ReadFactorial()
        {
            foreach (var token in ReadUnary())
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

            foreach (var token in ReadParentheses())
            {
                yield return token;
            }
        }

        private IEnumerable<Token> ReadParentheses()
        {
            SkipSpaces();
            if (myString[myIndex] == '(')
            {
                // (
                myIndex++;
                yield return new OpenParenthesisToken(myIndex - 1);

                // integer
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
            else
            {
                yield return ReadInteger();
            }
        }

        private IntToken ReadInteger()
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

            var result = new IntToken(number, myIndex);
            myIndex = nextToLast;

            return result;
        }
    }
}