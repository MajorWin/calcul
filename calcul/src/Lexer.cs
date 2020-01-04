using System;
using Calcul.Tokens;
using calcul.Tokens.OperationToken;

namespace Calcul
{
    public class Lexer : ILexer
    {
        private readonly string myString;
        private int myIndex;

        public Lexer(string s)
        {
            myString = s;
            Current = BofToken.Instance;
        }

        public Token Current { get; private set; }

        public Token GetNext()
        {
            return Current = NextToken();
        }


        private Token NextToken()
        {
            while (myIndex < myString.Length && myString[myIndex] == ' ')
            {
                myIndex++;
            }

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
                default:
                    if (!char.IsDigit(myString[myIndex]))
                    {
                        throw new Exception("!char.IsDigit(myString[myIndex])");
                    }

                    var numberLength = 0;
                    while (myIndex + numberLength < myString.Length &&
                           char.IsDigit(myString[myIndex + numberLength++])) ;

                    var number = int.Parse(myString.Substring(myIndex, numberLength));
                    myIndex += numberLength;

                    return new NumberToken(number);
            }
        }
    }
}