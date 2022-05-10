using System.Collections.Generic;
using Calcul.Lexer;
using Calcul.Lexer.Tokens;
using Calcul.Lexer.Tokens.SymbolTokens.Operations;
using Calcul.Lexer.Tokens.ValueTokens;
using NUnit.Framework;

namespace CalculTests.ArithmeticLexerTests;

[TestFixture]
public class SpaceTests
{
    public static readonly IEnumerable<TestCaseData> Expressions = new[]
    {
        new TestCaseData("Spaces before <end of statement>", "3! \n2!", new Token[]
            {
                new IntToken(0, 3),
                new ExclamationToken(1),
                new EndOfStatementToken(3, "\n"),
                new IntToken(4, 2),
                new ExclamationToken(5)
            }),
    };

    [TestCaseSource(nameof(Expressions))]
    public void ExpressionTest(string _, string expression, IEnumerable<Token> expectedTokens)
    {
        // given
        var lexer = new ArithmeticLexer(expression);

        // then
        lexer.GetNext();

        foreach (var token in expectedTokens)
        {
            Assert.That(lexer.Current, Is.Not.Null);
            Assert.That(lexer.Current, Is.EqualTo(token));
            lexer.GetNext();
        }

        Assert.That(lexer.Current?.GetType(), Is.EqualTo(typeof(EofToken)));
    }
}