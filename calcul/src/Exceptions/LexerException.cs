using System;

namespace Calcul.Exceptions;

public class LexerException : Exception
{
    public LexerException(string expected, string found, int offset) : base(
        $"Expected {expected}, found ${found} at ${offset}") { }

    public LexerException(string found, int offset) : base($"Found unexpected symbol ${found} at {offset}") { }
}