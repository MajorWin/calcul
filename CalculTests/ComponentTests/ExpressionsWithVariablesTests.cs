using System.Collections.Generic;
using Calcul.Lexer;
using Calcul.Parser;
using NUnit.Framework;

namespace CalculTests.ComponentTests;

[TestFixture]
public class ExpressionsWithVariablesTests
{
    private readonly Context myContext = new()
    {
        Variables = new Dictionary<string, int>
        {
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
        }
    };

    public static IEnumerable<TestCaseData> VariableExpressions = new[]
    {
        new TestCaseData("one + two**(-three+5)", 5)
    };

    [TestCaseSource(nameof(VariableExpressions)), Category(nameof(VariableExpressions))]
    public void ExpressionTest(string expression, int expectedResult)
    {
        // given
        var lexer = new ArithmeticLexer(expression);
        var parser = new InfixToAstParser(lexer, myContext);
        var expr = parser.ParseStatement();

        // then
        Assert.That(expr.Calculate(), Is.EqualTo(expectedResult));
    }
}