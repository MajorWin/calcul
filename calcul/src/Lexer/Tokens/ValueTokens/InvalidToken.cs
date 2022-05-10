namespace Calcul.Lexer.Tokens.ValueTokens;

public record InvalidToken(int Offset, string Value) : ValueToken<string>(Offset, Value)
{
    public override string StringRepresentation() => $"Invalid token: \"{Value}\"";
}