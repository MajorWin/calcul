namespace Calcul.Lexer.Tokens.KeywordTokens;

public abstract record KeywordToken(int Offset) : Token(Offset);