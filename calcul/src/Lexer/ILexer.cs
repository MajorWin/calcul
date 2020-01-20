using Calcul.Token;

namespace Calcul.Lexer
{
    public interface ILexer
    {
        IToken Current { get; }
        IToken GetNext();
    }
}