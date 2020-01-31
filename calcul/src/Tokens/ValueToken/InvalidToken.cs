namespace Calcul.Tokens.ValueToken
{
    public class InvalidToken : ValueToken<string>
    {
        public InvalidToken(string value, int offset) : base(value, offset) { }
        public override string ToString() => $"Invalid token: \"{Value}\"";
    }
}