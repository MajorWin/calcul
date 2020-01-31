namespace Calcul.Tokens.ValueToken
{
    public sealed class IntToken : ValueToken<int>
    {
        public IntToken(int value, int offset) : base(value, offset) { }
    }
}