namespace Calcul.Lexer.Tokens.Extensions;

public static class CharExtensions
{
    public static bool IsStartOfIdentifier(this char c) => char.IsLetter(c) || c == '_';
    public static bool IsIdentifierBody(this char c) => char.IsLetter(c) || c == '_' || char.IsNumber(c);
}