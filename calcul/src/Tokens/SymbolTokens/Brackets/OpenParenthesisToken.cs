namespace Calcul.Tokens.SymbolTokens.Brackets;

public record OpenParenthesisToken(int Offset): Token(Offset)
{
    public override string StringRepresentation() => "(";
}