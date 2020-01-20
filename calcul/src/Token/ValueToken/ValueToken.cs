namespace Calcul.Token.ValueToken
{
    public abstract class ValueToken<T> : IToken
    {
        public readonly T Value;
        protected ValueToken(T value) => Value = value;
        public override string ToString() => Value.ToString();
    }
}