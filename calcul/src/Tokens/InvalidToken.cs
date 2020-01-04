namespace Calcul.Tokens
{
    public class InvalidToken
    {
        public readonly string Value;

        public InvalidToken(string value)
        {
            Value = value;
        }

        public override string ToString() => $"Invalid token: \"{Value}\"";
    }
}