namespace Calcul.Tokens.ValueToken.Operations;

public sealed class PowerToken : Token
{
    public PowerToken(int offset) : base(offset) { }
    public override string ToString() => "**";
}