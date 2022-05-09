namespace Calcul.Expression;

public record IntNumberExpression(int Value) : IExpression
{
    private int Value { get; } = Value;
    public int Calculate() => Value;
    public override string ToString() => Value.ToString();
}