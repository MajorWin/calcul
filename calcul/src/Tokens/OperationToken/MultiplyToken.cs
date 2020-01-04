namespace calcul.Tokens.OperationToken {
    public sealed class MultiplyToken : OperationToken
    {
        public static readonly MultiplyToken Instance = new MultiplyToken();
        private MultiplyToken() => Value = "*";
    }
}