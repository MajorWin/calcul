namespace Calcul.Tokens;

public sealed class EofToken : Token
{
    public EofToken(int offset) : base(offset) { }
    public override string ToString() => "END";
}