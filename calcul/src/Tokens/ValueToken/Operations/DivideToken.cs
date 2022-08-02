namespace Calcul.Tokens.ValueToken.Operations;

public sealed class DivideToken : Token
{
    public DivideToken(int offset) : base(offset) { }
    public override string ToString() => "/";
}