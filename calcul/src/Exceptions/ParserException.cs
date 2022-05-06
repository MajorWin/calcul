using System;
using Calcul.Tokens;

namespace Calcul.Exceptions;

public class ParserException : Exception
{
    public ParserException(Type expected, Token found) : base(
        GetExeptionString(
            expected: expected.ToString(),
            found: found.ToString(),
            type: found.GetType().Name,
            offset: found.Offset)
    ) { }

    public ParserException((Type, Type) expecteds, Token found) : base(
        GetExeptionString(
            expected: (expecteds.Item1.Name, expecteds.Item2.Name).ToString(),
            found: found.ToString(),
            type: found.GetType().Name,
            offset: found.Offset)
    ) { }

    public ParserException((Type, Type, Type) expecteds, Token found) : base(
        GetExeptionString(
            expected: (expecteds.Item1.Name, expecteds.Item2.Name, expecteds.Item3.Name).ToString(),
            found: found.ToString(),
            type: found.GetType().Name,
            offset: found.Offset)
    ) { }

    private static string GetExeptionString(string expected, string found, string type, int offset) =>
        $"Expected {expected}, but found {found} of type {type} at {offset}";
}