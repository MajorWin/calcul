using Calcul.Expression;

namespace Calcul.Parser
{
    public interface IParser
    {
        IExpression Parse();
    }
}