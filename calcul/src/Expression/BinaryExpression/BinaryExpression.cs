namespace Calcul.Expression.BinaryExpression;

public abstract record BinaryExpression(IExpression Left, IExpression Right) : IExpression
{
    protected IExpression Left { get; } = Left;
    protected IExpression Right { get; } = Right;
    public abstract int Calculate();
}