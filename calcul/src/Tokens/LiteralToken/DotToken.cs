namespace calcul.Tokens.LiteralToken {
    public sealed class DotToken : calcul.Tokens.LiteralToken.LiteralToken
    {
        public static readonly DotToken Instance = new DotToken();
        private DotToken() => Value = ".";
    }
}