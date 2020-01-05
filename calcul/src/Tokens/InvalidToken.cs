namespace calcul.Tokens
{
    public class InvalidToken : Token
    {
        public readonly string Value;

        public InvalidToken(string value)
        {
            Value = value;
        }

        public override string ToString() => $"Invalid token: \"{Value}\"";
    }
}