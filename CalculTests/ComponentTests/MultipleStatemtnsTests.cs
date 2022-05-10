using System;
using System.Collections.Generic;
using Calcul.Lexer;
using Calcul.Parser;
using NUnit.Framework;

namespace CalculTests.ComponentTests;

[TestFixture]
public class MultipleStatementsTests
{
    public static IEnumerable<TestCaseData> Tests = new[]
    {
        new TestCaseData("Two arithmetic expressions", "1 + 2\n3!", new[] { 3, 6 })
    };

    [TestCaseSource(nameof(Tests))]
    public void MultipleStatementsTest(string _, string code, int[] resultValues)
    {
        var lexer = new ArithmeticLexer(code);
        var parser = new InfixToAstParser(lexer);

        var i = 0;
        foreach (var expression in parser.ParseStatements())
        {
            Assert.That(expression.Calculate(), Is.EqualTo(resultValues[i]));
            i++;
        }
    }
}