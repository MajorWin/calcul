namespace Calcul.Lexer.Tokens.SymbolTokens.Operations;

public record MinusToken(int Offset): Token(Offset)
{
    public override string StringRepresentation() => "-";
}