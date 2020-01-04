namespace Calcul.Tokens {
    public sealed class NumberToken : Token
    {
        public readonly int Value;

        public NumberToken(int value)
        {
            Value = value;
        }

        public override string ToString() => Value.ToString();
    }
}