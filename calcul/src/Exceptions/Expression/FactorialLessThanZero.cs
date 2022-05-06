using System;
using Calcul.Expression;

namespace Calcul.Exceptions.Expression;

public class FactorialLessThanZero : Exception
{
    public FactorialLessThanZero(IExpression operand, int value) : base(
        $"Can't calculate factorial of a negative number: {operand} = {value}") { }
}