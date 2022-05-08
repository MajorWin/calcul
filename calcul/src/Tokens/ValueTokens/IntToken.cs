namespace Calcul.Tokens.ValueTokens;

public record IntToken(int Offset, int Value): ValueToken<int>(Offset, Value);