using System.Collections.Generic;
using calcul.Tokens;

namespace calcul
{
    public interface IParser
    {
        IEnumerable<Token> Parse();
    }
}