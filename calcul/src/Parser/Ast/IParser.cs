using System.Collections.Generic;
using Calcul.Expression;
using Calcul.Token;

namespace Calcul.Parser.Ast
{
    public interface IParser
    {
        IExpression Parse();
    }
}