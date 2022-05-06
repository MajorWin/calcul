namespace Calcul.Tokens.ValueTokens;

public record IntToken(int Value, int Offset): ValueToken<int>(Value, Offset);