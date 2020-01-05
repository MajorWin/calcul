﻿using calcul.Tokens;
using calcul.Tokens.LiteralToken;

namespace calcul
{
    public class ArithmeticLexer : ILexer
    {
        private readonly string myString;
        private int myIndex;

        public ArithmeticLexer(string s)
        {
            myString = s;
            Current = BofToken.Instance;
        }

        public Token Current { get; private set; }

        public Token GetNext() => Current = ReadNextToken();

        private Token ReadNextToken()
        {
            SkipSpaces();

            if (myIndex == myString.Length)
            {
                return EofToken.Instance;
            }

            switch (myString[myIndex])
            {
                case '+':
                    myIndex++;
                    return PlusToken.Instance;
                case '-':
                    myIndex++;
                    return MinusToken.Instance;
                case '*':
                    myIndex++;
                    return MultiplyToken.Instance;
                case '/':
                    myIndex++;
                    return DivideToken.Instance;
                case '.':
                    //TODO: floating point numbers
                    myIndex++;
                    return DotToken.Instance;
                default:
                    if (!char.IsDigit(myString[myIndex]))
                    {
                        myIndex++;
                        return new InvalidToken(myString[myIndex - 1].ToString());
                    }

                    return ReadInt();
            }
        }
        
        private void SkipSpaces()
        {
            while (myString.Length > myIndex && myString[myIndex] == ' ')
            {
                myIndex++;
            }
        }

        private IntToken ReadInt()
        {
            var nextToLast = myIndex + 1;
            while (nextToLast < myString.Length && char.IsDigit(myString[nextToLast]))
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