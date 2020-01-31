namespace Calcul.Token.ValueToken.Operations
{
    public sealed class MultiplyToken : ValueToken<char>
    {
        public static readonly MultiplyToken Instance = new MultiplyToken();
        private MultiplyToken() : base('*') { }
    }
}