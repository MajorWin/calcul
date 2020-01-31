namespace Calcul.Tokens.ValueToken.Operations
{
    public sealed class ExclamationToken : ValueToken<char>
    {
        public static readonly ExclamationToken Instance = new ExclamationToken();
        private ExclamationToken() : base('!') { }
    }
}