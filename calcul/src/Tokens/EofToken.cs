namespace Calcul.Tokens;

public record EofToken(int Offset) : Token(Offset)
{
    public override string StringRepresentation() => "END";
}