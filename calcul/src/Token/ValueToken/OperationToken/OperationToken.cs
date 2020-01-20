namespace Calcul.Token.ValueToken.OperationToken
{
    public abstract class OperationToken : ValueToken<char>
    {
        protected OperationToken(char value) : base(value) { }
    }
}