using System;

namespace Calcul.Lexer.Tokens;

public record EofToken(int Offset) : EndOfStatementToken(Offset, $"{Environment.NewLine}END")
{
    public override string StringRepresentation() => Value;
}