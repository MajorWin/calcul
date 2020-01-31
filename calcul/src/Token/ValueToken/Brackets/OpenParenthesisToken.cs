namespace Calcul.Token.ValueToken.Brackets
{
    public sealed class OpenParenthesisToken : ValueToken<char>
    {
        public static readonly OpenParenthesisToken Instance = new OpenParenthesisToken();
        public OpenParenthesisToken() : base('(') { }
    }
}