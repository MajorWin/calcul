using System;

namespace Calcul.Exceptions.Expression;

public class UnknownVariableException : Exception
{
    public UnknownVariableException(string name) : base($"Unknown variable '{name}'") { }
}