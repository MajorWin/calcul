namespace Calcul.Tokens.ValueToken
{
    public abstract class ValueToken<T> : Token
    {
        public readonly T Value;

        protected ValueToken(T value, int offset) : base(offset) => Value = value;
        public override string ToString() => Value.ToString();
    }
}