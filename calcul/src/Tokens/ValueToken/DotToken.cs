namespace Calcul.Tokens.ValueToken;

public sealed class DotToken : Token
{
    private DotToken(int offset) : base(offset) { }
    public override string ToString() => ".";
}