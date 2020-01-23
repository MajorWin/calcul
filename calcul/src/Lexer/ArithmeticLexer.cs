using Calcul.Token;
using Calcul.Token.ValueToken;
using Calcul.Token.ValueToken.OperationToken;

namespace Calcul.Lexer
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

        public IToken Current { get; private set; }

        public IToken GetNext() => Current = ReadNextToken();
        
        public IToken GetCurrentAndMoveNext()
        {
            var oldCurrent = Current;
            Current = ReadNextToken();
            return oldCurrent;
        }

        private IToken ReadNextToken()
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
                case '(':
                    myIndex++;
                    return OpenParenthesisToken.Instance;
                case ')':
                    myIndex++;
                    return CloseParenthesisToken.Instance;
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
            while (myString.Length > myIndex && char.IsWhiteSpace(myString[myIndex]))
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