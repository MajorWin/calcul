namespace Calcul.Tokens.ValueToken.Operations;

public sealed class ExclamationToken : Token
{
    public ExclamationToken(int offset) : base(offset) { }
    public override string ToString() => "!";
}