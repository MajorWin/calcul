using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Calcul.Extensions;
using Calcul.Lexer;
using Calcul.Token;
using Calcul.Token.ValueToken;

namespace Calcul.Parser
{
    public class InfixToPostfixParser : IParser
    {
        private readonly ILexer myLexer;

        public InfixToPostfixParser(ILexer lexer) => myLexer = lexer;

        public IReadOnlyList<IToken> Parse()
        {
            if (myLexer.GetNext() is EofToken)
            {
                return ImmutableList<IToken>.Empty;
            }

            var result = new List<IToken>();
            ParseExpr(result);
            return result;
        }

        private void ParseExpr(List<IToken> result)
        {
            ParseTerm(result);
            while (myLexer.Current.IsExprToken())
            {
                var operation = myLexer.GetCurrentAndMoveNext();
                ParseTerm(result);
                result.Add(operation);
            }
        }
        
        private void ParseTerm(List<IToken> result)
        {
            ParseFactory(result);
            while (myLexer.Current.IsTermToken())
            {
                var operation = myLexer.GetCurrentAndMoveNext();
                ParseFactory(result);
                result.Add(operation);
            }
        }
        
        private void ParseFactory(List<IToken> result)
        {
            var factory = myLexer.GetCurrentAndMoveNext();
            if (!(factory is IntToken))
            {
                throw new Exception($"!(token is IntToken): {factory}");
            }

            result.Add(factory);
        }
    }
}