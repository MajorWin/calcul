using System;
using Calcul.Tokens;

namespace Calcul.Exceptions
{
    public class ParserException : Exception
    {
        public ParserException(Type expected, Token found) : base(
            $"Expected {expected}, but found {found} of type {found.GetType().Name}") { }

        public ParserException((Type, Type) expecteds, Token found) : base(
            $"Expected one of {(expecteds.Item1.Name, expecteds.Item2.Name)}, but found {found} of type {found.GetType().Name}") { }

        public ParserException((Type, Type, Type) expecteds, Token found) : base(
            $"Expected one of {(expecteds.Item1.Name, expecteds.Item2.Name, expecteds.Item3.Name)}, but found {found} of type {found.GetType().Name}") { }
    }
}