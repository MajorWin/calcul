namespace Calcul.Tokens.ValueTokens;

public record InvalidToken(string Value, int Offset) : ValueToken<string>(Value, Offset)
{
    public override string StringRepresentation() => $"Invalid token: \"{Value}\"";
}