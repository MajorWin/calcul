namespace Calcul.Lexer.Tokens.SymbolTokens.Operations;

public record PlusToken(int Offset): Token(Offset)
{
    public override string StringRepresentation() => "+";
}