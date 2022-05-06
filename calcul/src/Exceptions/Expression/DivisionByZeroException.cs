using System;
using Calcul.Expression;

namespace Calcul.Exceptions.Expression;

public class DivisionByZeroException : Exception
{
    public DivisionByZeroException(IExpression left, IExpression right) : base(
        $"Divisor is equals to zero: ({left}) / ({right})") { }
}