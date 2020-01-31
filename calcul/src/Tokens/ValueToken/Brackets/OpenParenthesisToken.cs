namespace Calcul.Tokens.ValueToken.Brackets
{
    public sealed class OpenParenthesisToken : Token
    {
        public OpenParenthesisToken(int offset) : base(offset) { }
        public override string ToString() => "(";
    }
}