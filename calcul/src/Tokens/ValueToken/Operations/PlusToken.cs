namespace Calcul.Tokens.ValueToken.Operations;

public sealed class PlusToken : Token
{
    public PlusToken(int offset) : base(offset) { }
    public override string ToString() => "/";
}