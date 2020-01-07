namespace Calcul.Tokens.ValueToken
{
    public class InvalidToken : ValueToken<string>
    {
        public InvalidToken(string value) : base(value) { }
        public override string ToString() => $"Invalid token: \"{Value}\"";
    }
}