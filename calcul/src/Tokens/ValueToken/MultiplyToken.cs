namespace Calcul.Tokens.ValueToken
{
    public sealed class MultiplyToken : ValueToken<char>
    {
        public static readonly MultiplyToken Instance = new MultiplyToken();
        private MultiplyToken() : base('*') { }
    }
}