using System.Collections.Generic;

namespace Calcul.Parser;

public interface IContext
{
    public Dictionary<string, int> Variables { get; }
}