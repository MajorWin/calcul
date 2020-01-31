namespace Calcul.Tokens.ValueToken.Operations
{
    public sealed class MinusToken : ValueToken<char>
    {
        public static readonly MinusToken Instance = new MinusToken();
        private MinusToken() : base('-') { }
    }
}