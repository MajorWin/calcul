namespace calcul.Tokens.LiteralToken {
    public sealed class MinusToken : LiteralToken
    {
        public static readonly MinusToken Instance = new MinusToken();
        private MinusToken() => Value = "-";
    }
}