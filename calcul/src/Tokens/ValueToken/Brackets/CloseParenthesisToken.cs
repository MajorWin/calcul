namespace Calcul.Tokens.ValueToken.Brackets;

public sealed class CloseParenthesisToken : Token
{
    public CloseParenthesisToken(int offset) : base(offset) { }
    public override string ToString() => ")";
}