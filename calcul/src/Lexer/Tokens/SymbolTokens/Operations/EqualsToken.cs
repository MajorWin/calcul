namespace Calcul.Lexer.Tokens.SymbolTokens.Operations;

public record EqualsToken(int Offset) : Token(Offset)
{
    public override string StringRepresentation() => "=";
}