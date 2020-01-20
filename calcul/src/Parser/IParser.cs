using System.Collections.Generic;
using Calcul.Token;

namespace Calcul.Parser
{
    public interface IParser
    {
        IReadOnlyList<IToken> Parse();
    }
}