using Calcul.Tokens;
using Calcul.Tokens.SymbolTokens.Operations;

namespace Calcul.Extensions;

public static class TokenHelper
{
    public static bool IsAdditiveToken(this Token token) => IsPlusMinus(token);

    public static bool IsMultiplicativeToken(this Token token)
    {
        return token is MultiplyToken || token is DivideToken;
    }

    public static bool IsUnary(this Token token) => IsPlusMinus(token);

    private static bool IsPlusMinus(Token token)
    {
        return token is PlusToken || token is MinusToken;
    }

    public static bool Is<T>(this Token token) where T : Token => token is T;

    public static bool IsNot<T>(this Token token) where T : Token => !token.Is<T>();
}