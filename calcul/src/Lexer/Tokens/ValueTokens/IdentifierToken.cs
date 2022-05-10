namespace Calcul.Lexer.Tokens.ValueTokens;

public record IdentifierToken(int Offset, string Value): ValueToken<string>(Offset, Value);
