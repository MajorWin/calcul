namespace calcul.Tokens.LiteralToken {
    public sealed class PlusToken : LiteralToken
    {
        public static readonly PlusToken Instance = new PlusToken();
        private PlusToken() => Value = "+";
    }
}