namespace calcul.Tokens.LiteralToken {
    public sealed class MultiplyToken : LiteralToken
    {
        public static readonly MultiplyToken Instance = new MultiplyToken();
        private MultiplyToken() => Value = "*";
    }
}