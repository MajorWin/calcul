using calcul.Tokens;

namespace calcul
{
    public interface ILexer
    {
        Token Current { get; }
        Token GetNext();
    }
}