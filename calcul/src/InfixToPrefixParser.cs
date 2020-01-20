﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Calcul.Tokens;

namespace Calcul
{
    class InfixToPrefixParser : IParser
    {
        private readonly ILexer myLexer;

        public InfixToPrefixParser(ILexer lexer) => myLexer = lexer;

        public IReadOnlyList<IToken> Parse()
        {
            return myLexer.GetNext() is EofToken ? (IReadOnlyList<IToken>) ImmutableList<IToken>.Empty : ParseExpr();
        }

        private List<IToken> ParseExpr()
        {
            var expr = new List<IToken>();
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

        private List<IToken> ParseTerm()
        {
            var term = new List<IToken>();
            var operands = new List<IToken> {ParseFactory()};
            while (myLexer.Current.IsMultiplyDivide())
            {
                term.Add(myLexer.Current);
                myLexer.GetNext();
                operands.Add(ParseFactory());
            }

            term.AddRange(operands);
            return term;
        }

        private IToken ParseFactory()
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