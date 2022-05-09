using System.Collections.Generic;
using Calcul.Lexer;
using Calcul.Parser;
using NUnit.Framework;

namespace CalculTests.ComponentTests;

[TestFixture]
public class InfixToAstParserTests
{
    public static readonly IEnumerable<TestCaseData> SingleExpressions = new[]
    {
        new TestCaseData(" 2  +  4+6", 12),
        new TestCaseData(" 2  -  4-6", -8),
        new TestCaseData(" 2  *  4*6", 48),
        new TestCaseData(" 12  /  2/3", 2),
        new TestCaseData(" 2  **  3**2", 512),
        new TestCaseData("  ++  2", 2),
        new TestCaseData("---2  ", -2),
        new TestCaseData("3!!  ", 720),
        new TestCaseData("((2))", 2),
        new TestCaseData("12345", 12345),
    };

    public static IEnumerable<TestCaseData> GrammarSameLevelExpressions = new[]
    {
    new TestCaseData("1+2-3", 0),
    new TestCaseData("1*6/2", 3),
    new TestCaseData("+-1", -1),
    };

    public static IEnumerable<TestCaseData> GrammarConsecutiveExpressions = new[]
    {
    new TestCaseData("1*2+3*4", 14),
    new TestCaseData("6/3-6/2", -1),
    new TestCaseData("2**2*3**4", 324),
    new TestCaseData("6**2/2**2", 9),
    new TestCaseData("-2**3", -8),
    new TestCaseData("+3!", 6),
    new TestCaseData("-3!", -6),
    new TestCaseData("0!", 1),
    new TestCaseData("-(1)", -1),
    new TestCaseData("(1+2)", 3),
    };

    [TestCaseSource(nameof(SingleExpressions)), Category(nameof(SingleExpressions))]
    [TestCaseSource(nameof(GrammarSameLevelExpressions)), Category(nameof(GrammarSameLevelExpressions))]
    [TestCaseSource(nameof(GrammarConsecutiveExpressions)), Category(nameof(GrammarConsecutiveExpressions))]
    public void ExpressionTest(string expression, int expectedResult)
    {
        // given
        var lexer = new ArithmeticLexer(expression);
        var parser = new InfixToAstParser(lexer);
        var expr = parser.Parse();

        // then
        Assert.That(expr.Calculate(), Is.EqualTo(expectedResult));
    }
}