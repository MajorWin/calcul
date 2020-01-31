using System;
using System.Collections.Generic;
using Calcul.Exceptions;
using Calcul.Token;
using Calcul.Token.ValueToken;
using Calcul.Token.ValueToken.Brackets;
using Calcul.Token.ValueToken.Operations;

namespace Calcul.Lexer
{
    public class ArithmeticLexer : ILexer
    {
        private readonly string myString;
        private int myIndex;
        private readonly IEnumerator<Token.IToken> myTokens;

        public ArithmeticLexer(string s)
        {
            myString = s + '\0';
            Current = BofToken.Instance;
            myTokens = ReadAdditive().GetEnumerator();
        }

        public Token.IToken Current { get; private set; }

        public Token.IToken GetNext()
        {
            var hasNext = myTokens.MoveNext();
            if (!hasNext && myIndex != myString.Length - 1)
            {
                throw new LexerException(myString[myIndex].ToString(), myIndex);
            }
            Current = hasNext ? myTokens.Current : EofToken.Instance;
            return Current;
        }

        public Token.IToken GetCurrentAndMoveNext()
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

        private IEnumerable<Token.IToken> ReadAdditive()
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
                    '+' => PlusToken.Instance,
                    _ => MinusToken.Instance
                };
            }
        }

        private IEnumerable<Token.IToken> ReadMultiplicative()
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
                    '*' => MultiplyToken.Instance,
                    _ => DivideToken.Instance
                };
            }
        }

        private IEnumerable<Token.IToken> ReadPower()
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
                yield return PowerToken.Instance;
            }
        }

        private IEnumerable<Token.IToken> ReadFactorial()
        {
            foreach (var token in ReadUnary())
            {
                yield return token;
            }

            SkipSpaces();
            while (myString[myIndex] == '!')
            {
                myIndex++;
                yield return ExclamationToken.Instance;
                SkipSpaces();
            }
        }

        private IEnumerable<Token.IToken> ReadUnary()
        {
            SkipSpaces();
            while (myString[myIndex] == '+' || myString[myIndex] == '-')
            {
                myIndex++;
                yield return myString[myIndex - 1] == '+' ? (Token.IToken) PlusToken.Instance : MinusToken.Instance;
                SkipSpaces();
            }

            foreach (var token in ReadParentheses())
            {
                yield return token;
            }
        }

        private IEnumerable<Token.IToken> ReadParentheses()
        {
            SkipSpaces();
            if (myString[myIndex] == '(')
            {
                // (
                myIndex++;
                yield return OpenParenthesisToken.Instance;

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
                    yield return CloseParenthesisToken.Instance;
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
            myIndex = nextToLast;

            return new IntToken(number);
        }
    }
}