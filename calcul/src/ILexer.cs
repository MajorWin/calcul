using Calcul.Tokens;

namespace Calcul
{
    public interface ILexer
    {
        IToken Current { get; }
        IToken GetNext();
    }
}