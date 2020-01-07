using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Calcul.Tokens;

namespace Calcul
{
    class InfixToPrefixParser : IParser
    {
        private readonly ILexer myLexer;

        public InfixToPrefixParser(ILexer lexer) => myLexer = lexer;

        public IReadOnlyList<Token> Parse()
        {
            return myLexer.GetNext() is EofToken ? (IReadOnlyList<Token>) ImmutableList<Token>.Empty : ParseExpr();
        }

        private List<Token> ParseExpr()
        {
            var expr = new List<Token>();
            var operands = ParseTerm();
            while (myLexer.Current.IsPlusMinus())
            {
                expr.Add(myLexer.Current);
                myLexer.GetNext();
                operands.AddRange(ParseTerm());
            }

            expr.AddRange(operands);
            return expr;
        }

        private List<Token> ParseTerm()
        {
            var term = new List<Token>();
            var operands = new List<Token> {ParseFactory()};
            while (myLexer.Current.IsMultiplyDivide())
            {
                term.Add(myLexer.Current);
                myLexer.GetNext();
                operands.Add(ParseFactory());
            }

            term.AddRange(operands);
            return term;
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
    }
}