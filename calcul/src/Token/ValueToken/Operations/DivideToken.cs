namespace Calcul.Token.ValueToken.Operations
{
    public sealed class DivideToken : ValueToken<char>
    {
        public static readonly DivideToken Instance = new DivideToken();
        private DivideToken() : base('/') { }
    }
}