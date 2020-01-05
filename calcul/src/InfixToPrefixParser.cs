using System;
using System.Collections.Generic;
using System.Linq;
using calcul.Tokens;
using calcul.Tokens.LiteralToken;

namespace calcul
{
    class InfixToPrefixParser : IParser
    {
        private readonly ILexer myLexer;

        public InfixToPrefixParser(ILexer lexer) => myLexer = lexer;

        public IEnumerable<Token> Parse()
        {
            return (myLexer.GetNext() is EofToken) ? Enumerable.Empty<Token>() : ParseExpr();
        }

        private List<Token> ParseExpr()
        {
            var expr = new List<Token>();
            var operands = ParseTerm();
            while (isPlusMinus(myLexer.Current))
            {
                expr.Add(myLexer.Current);
                myLexer.GetNext();
                operands.AddRange(ParseTerm());
            }

            return expr.Concat(operands).ToList();
        }

        private List<Token> ParseTerm()
        {
            var term = new List<Token>();
            var operands = new List<Token> {ParseFactory()};
            while (isMultiplyDivide(myLexer.Current))
            {
                term.Add(myLexer.Current);
                myLexer.GetNext();
                operands.Add(ParseFactory());
            }

            return term.Concat(operands).ToList();
        }

        private Token ParseFactory()
        {
            var token = myLexer.Current;
            if (!(token is IntToken))
            {
                throw new Exception($"!(token is IntToken): {token}");
            }

            myLexer.GetNext();
            return token;
        }

        private bool isPlusMinus(Token token)
        {
            return token is PlusToken || token is MinusToken;
        }

        private bool isMultiplyDivide(Token token)
        {
            return token is MultiplyToken || token is DivideToken;
        }
    }
}