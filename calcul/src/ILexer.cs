using Calcul.Tokens;

namespace Calcul
{
    public interface ILexer
    {
        Token Current { get; }
        Token GetNext();
    }
}