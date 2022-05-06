namespace Calcul.Tokens.ValueTokens;

public record ValueToken<T>(T Value, int Offset): Token(Offset)
{
    public override string StringRepresentation() => Value.ToString();
}