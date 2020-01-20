namespace Calcul.Token.ValueToken.OperationToken
{
    public sealed class DivideToken : OperationToken
    {
        public static readonly DivideToken Instance = new DivideToken();
        private DivideToken() : base('/') { }
    }
}