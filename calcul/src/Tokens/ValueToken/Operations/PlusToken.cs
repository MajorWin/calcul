namespace Calcul.Tokens.ValueToken.Operations
{
    public sealed class PlusToken : ValueToken<char>
    {
        public static readonly PlusToken Instance = new PlusToken();
        private PlusToken() : base('+') { }
    }
}