namespace Calcul.Tokens;

public sealed class BofToken : Token
{
    public static readonly BofToken Instance = new();

    private BofToken() : base(-1) { }
    public override string ToString() => "BEGIN";
}