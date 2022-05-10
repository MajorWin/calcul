namespace Calcul.Lexer.Tokens.SymbolTokens;

public record DotToken(int Offset): Token(Offset)
{
    public override string StringRepresentation() => ".";
}