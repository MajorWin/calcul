namespace Calcul.Tokens {
    public sealed class EofToken : Token
    {
        public static readonly EofToken Instance = new EofToken();
        public override string ToString() => "}";
    }
}