namespace calcul.Tokens.LiteralToken {
    public sealed class DivideToken : LiteralToken
    {
        public static readonly DivideToken Instance = new DivideToken();
        private DivideToken() => Value = "/";
    }
}