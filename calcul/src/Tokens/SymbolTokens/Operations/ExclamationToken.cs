namespace Calcul.Tokens.SymbolTokens.Operations;

public record ExclamationToken(int Offset): Token(Offset)
{
    public override string StringRepresentation() => "!";
}