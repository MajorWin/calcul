namespace Calcul.Tokens
{
    public sealed class BofToken : Token
    {
        public static readonly BofToken Instance = new BofToken();
        public override string ToString() => "BEGIN";
    }
}