namespace Calcul.Lexer.Tokens;

public record EndOfStatementToken(int Offset, string Value) : Token(Offset)
{
    public override string StringRepresentation() => Value;
}