namespace Calcul.Lexer.Tokens.ValueTokens;

public record ValueToken<T>(int Offset, T Value): Token(Offset)
{
    public override string StringRepresentation() => Value.ToString();
}