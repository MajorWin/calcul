namespace Calcul.Lexer.Tokens;

public record BofToken(int Offset) : Token(Offset)
{
    public static readonly BofToken Instance = new(-1);

    public override string StringRepresentation() => "BEGIN";
}