using Calcul.Tokens;

namespace Calcul.Lexer
{
    public interface ILexer
    {
        Token Current { get; }
        Token GetNext();
        Token GetCurrentAndMoveNext();
    }
}