namespace Calcul.Tokens.ValueToken
{
    public sealed class PlusToken : ValueToken<char>
    {
        public static readonly PlusToken Instance = new PlusToken();
        private PlusToken() : base('+') { }
    }
}