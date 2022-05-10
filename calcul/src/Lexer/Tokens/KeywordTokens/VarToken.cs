namespace Calcul.Lexer.Tokens.KeywordTokens;

public record VarToken(int Offset) : KeywordToken(Offset)
{
    public override string StringRepresentation() => $"__{Keywords.Var}__";
}