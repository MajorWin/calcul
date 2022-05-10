namespace Calcul.Lexer.Tokens;

public abstract record Token(int Offset)
{
    public abstract string StringRepresentation();
}