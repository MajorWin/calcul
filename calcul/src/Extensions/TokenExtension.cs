using Calcul.Token;
using Calcul.Token.ValueToken.Operations;

namespace Calcul.Extensions
{
    public static class TokenHelper
    {
        public static bool IsAdditiveToken(this Token.IToken token) => IsPlusMinus(token);

        public static bool IsMultiplicativeToken(this Token.IToken token)
        {
            return token is MultiplyToken || token is DivideToken;
        }

        public static bool IsUnary(this Token.IToken token) => IsPlusMinus(token);

        private static bool IsPlusMinus(Token.IToken token)
        {
            return token is PlusToken || token is MinusToken;
        }
        
        public static bool Is<T>(this Token.IToken token) where T : Token.IToken => token is T;
        
        public static bool IsNot<T>(this Token.IToken token) where T : Token.IToken => !token.Is<T>();
    }
}