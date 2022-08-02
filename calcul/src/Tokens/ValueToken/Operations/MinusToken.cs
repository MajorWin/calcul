namespace Calcul.Tokens.ValueToken.Operations;

public sealed class MinusToken : Token
{
    public MinusToken(int offset) : base(offset) { }
    public override string ToString() => "/";
}