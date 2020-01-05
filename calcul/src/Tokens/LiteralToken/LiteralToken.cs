namespace calcul.Tokens.LiteralToken
{
    public abstract class LiteralToken : Token
    {
        public string Value { get; protected set; }
        public override string ToString() => Value;
    }
}