namespace calcul.Tokens.OperationToken {
    public sealed class MinusToken : OperationToken
    {
        public static readonly MinusToken Instance = new MinusToken();
        private MinusToken() => Value = "-";
    }
}