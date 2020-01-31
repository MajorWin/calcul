namespace Calcul.Tokens.ValueToken
{
    public sealed class DotToken : ValueToken<char>
    {
        public static readonly DotToken Instance = new DotToken();
        private DotToken() : base('.') { }
    }
}