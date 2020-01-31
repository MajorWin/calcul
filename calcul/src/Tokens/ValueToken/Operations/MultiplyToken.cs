namespace Calcul.Tokens.ValueToken.Operations
{
    public sealed class MultiplyToken : Token
    {
        public MultiplyToken(int offset) : base(offset) { }
        public override string ToString() => "*";
    }
}