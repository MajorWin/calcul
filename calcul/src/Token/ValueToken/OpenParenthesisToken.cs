namespace Calcul.Token.ValueToken
{
    public class OpenParenthesisToken : ValueToken<char>
    {
        public static readonly OpenParenthesisToken Instance = new OpenParenthesisToken();
        public OpenParenthesisToken() : base('(') { }
    }
}