namespace Calcul.Token.ValueToken
{
    public class CloseParenthesisToken : ValueToken<char>
    {
        public static readonly CloseParenthesisToken Instance = new CloseParenthesisToken();
        public CloseParenthesisToken() : base(')') { }
    }
}