namespace Calcul.Token.ValueToken.Brackets
{
    public sealed class CloseParenthesisToken : ValueToken<char>
    {
        public static readonly CloseParenthesisToken Instance = new CloseParenthesisToken();
        public CloseParenthesisToken() : base(')') { }
    }
}