using Calcul.Parser.Expressions;

namespace Calcul.Parser;

public interface IParser
{
    IExpression Parse();
}