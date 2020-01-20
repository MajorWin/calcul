using Calcul.Token;
using Calcul.Token.ValueToken.OperationToken;

namespace Calcul.Extensions
{
    public static class TokenHelper
    {
        public static bool IsExprToken(this IToken token)
        {
            return token is PlusToken || token is MinusToken;
        }

        public static bool IsTermToken(this IToken token)
        {
            return token is MultiplyToken || token is DivideToken;
        }
        
        public static bool Is<T>(this IToken token) where T : IToken => token is T;
        
        public static bool IsNot<T>(this IToken token) where T : IToken => !token.Is<T>();
    }
}