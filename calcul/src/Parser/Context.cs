using System.Collections.Generic;

namespace Calcul.Parser;

public class Context : IContext
{
    public Dictionary<string, int> Variables { get; init; }
}