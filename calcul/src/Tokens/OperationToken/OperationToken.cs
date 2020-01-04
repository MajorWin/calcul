using Calcul.Tokens;

namespace calcul.Tokens.OperationToken
{
    public abstract class OperationToken : Token
    {
        public string Value { get; protected set; }
        public override string ToString() => Value;
    }
}