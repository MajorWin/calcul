using Calcul.Tokens.ValueToken;
using Calcul.Tokens.ValueToken.OperationToken;

namespace Calcul.Tokens
{
    public static class TokenHelper
    {
        public static bool IsPlusMinus(this IToken token)
        {
            return token is PlusToken || token is MinusToken;
        }

        public static bool IsMultiplyDivide(this IToken token)
        {
            return token is MultiplyToken || token is DivideToken;
        }
        
        public static bool Is<T>(this IToken token) where T : IToken => token is T;
        
        public static bool IsNot<T>(this IToken token) where T : IToken => !token.Is<T>();
    }
}