using System;
using Calcul.Token;

namespace Calcul.Exceptions
{
    public class ParserException : Exception
    {
        public ParserException(Type expected, Token.IToken found) : base(
            $"Expected {expected}, but found {found} of type {found.GetType().Name}") { }

        public ParserException((Type, Type) expecteds, Token.IToken found) : base(
            $"Expected one of {(expecteds.Item1.Name, expecteds.Item2.Name)}, but found {found} of type {found.GetType().Name}") { }

        public ParserException((Type, Type, Type) expecteds, Token.IToken found) : base(
            $"Expected one of {(expecteds.Item1.Name, expecteds.Item2.Name, expecteds.Item3.Name)}, but found {found} of type {found.GetType().Name}") { }
    }
}