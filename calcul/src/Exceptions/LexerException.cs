using System;

namespace Calcul.Exceptions
{
    public class LexerException : Exception
    {
        public LexerException(string expected, string found, int at) : base(
            $"Expected {expected}, found ${found} at ${at}") { }

        public LexerException(string found, int at) : base($"Found unexpected symbol ${found} at {at}") { }
    }
}