namespace calcul.Tokens
{
    public sealed class IntToken : Token
    {
        public readonly int Value;

        public IntToken(int value)
        {
            Value = value;
        }

        public override string ToString() => Value.ToString();
    }
}