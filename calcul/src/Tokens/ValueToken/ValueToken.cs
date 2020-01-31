namespace Calcul.Tokens.ValueToken
{
    public abstract class ValueToken<T> : Token
    {
        public readonly T Value;
        protected ValueToken(T value) => Value = value;
        public override string ToString() => Value.ToString();
    }
}