namespace Calcul.Tokens.SymbolTokens.Operations;

public record PowerToken(int Offset): Token(Offset)
{
    public override string StringRepresentation() => "^";
}