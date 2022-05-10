namespace Calcul.Parser.Expressions;

public class VariableExpression : IExpression
{
    public VariableExpression(string name, int value)
    {
        Name = name;
        Value = value;
    }

    public int Calculate() => Value;

    public override string ToString() => $"({Name}: {Value})";
    public string Name { get; init; }
    public int Value { get; init; }
}