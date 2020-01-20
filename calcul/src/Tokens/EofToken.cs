namespace Calcul.Tokens
{
    public sealed class EofToken : IToken
    {
        public static readonly EofToken Instance = new EofToken();
        public override string ToString() => "END";
    }
}