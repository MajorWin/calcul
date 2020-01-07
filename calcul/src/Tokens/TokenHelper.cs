using Calcul.Tokens.ValueToken;

namespace Calcul.Tokens
{
    public static class TokenHelper
    {
        public static bool IsPlusMinus(this Token token)
        {
            return token is PlusToken || token is MinusToken;
        }

        public static bool IsMultiplyDivide(this Token token)
        {
            return token is MultiplyToken || token is DivideToken;
        }
        
        public static bool Is<T>(this Token token) where T : Token => token is T;
        
        public static bool IsNot<T>(this Token token) where T : Token => !token.Is<T>();
    }
}