using System.Collections.Generic;
using Calcul.Lexer;
using Calcul.Lexer.Tokens;
using Calcul.Lexer.Tokens.SymbolTokens.Brackets;
using Calcul.Lexer.Tokens.SymbolTokens.Operations;
using Calcul.Lexer.Tokens.ValueTokens;
using NUnit.Framework;

namespace CalculTests.ArithmeticLexerTests;

[TestFixture]
public class ArithmeticLexerPositiveTests
{
    [Test]
    public void TestBofEof()
    {
        // given
        const string s = " 2    ";
        var lexer = new ArithmeticLexer(s);

        // then
        Assert.That(lexer.Current.GetType(), Is.EqualTo(typeof(BofToken)));
        lexer.GetNext();

        lexer.GetNext(); // skip number

        Assert.That(lexer.Current.GetType(), Is.EqualTo(typeof(EofToken)));

        lexer.GetNext(); // check Eof is a final state
        Assert.That(lexer.Current.GetType(), Is.EqualTo(typeof(EofToken)));
    }

    public static readonly IEnumerable<TestCaseData> SingleExpressions = new[]
    {
        new TestCaseData(" 2  +  4+6", new Token[] { new IntToken(1, 2), new PlusToken(4), new IntToken(7, 4), new PlusToken(8), new IntToken(9, 6) }),
        new TestCaseData(" 2  -  4-6", new Token[] { new IntToken(1, 2), new MinusToken(4), new IntToken(7, 4), new MinusToken(8), new IntToken(9, 6)  }),
        new TestCaseData(" 2  *  4*6", new Token[] { new IntToken(1, 2), new MultiplyToken(4), new IntToken(7, 4), new MultiplyToken(8), new IntToken(9, 6) }),
        new TestCaseData(" 2  /  4/6", new Token[] { new IntToken(1, 2), new DivideToken(4), new IntToken(7, 4), new DivideToken(8), new IntToken(9, 6) }),
        new TestCaseData(" 2  **  4**6", new Token[] { new IntToken(1, 2), new PowerToken(4), new IntToken(8, 4), new PowerToken(9), new IntToken(11, 6) }),
        new TestCaseData("  ++  2", new Token[] { new PlusToken(2), new PlusToken(3), new IntToken(6, 2) }),
        new TestCaseData("--2  ", new Token[] { new MinusToken(0), new MinusToken(1), new IntToken(2, 2) }),
        new TestCaseData("2!!  ", new Token[] { new IntToken(0, 2), new ExclamationToken(1), new ExclamationToken(2) }),
        new TestCaseData("((2))", new Token[]
        {
            new OpenParenthesisToken(0),
            new OpenParenthesisToken(1),
            new IntToken(2, 2),
            new CloseParenthesisToken(3),
            new CloseParenthesisToken(4)
        }),
        new TestCaseData("12345", new Token[] { new IntToken(0, 12345) }),
        new TestCaseData("_asdf123", new Token[] { new IdentifierToken(0, "_asdf123")})
    };

    public static IEnumerable<TestCaseData> GrammarSameLevelExpressions = new[]
    {
        new TestCaseData("1+2-3", new Token[]
        {
            new IntToken(0, 1),
            new PlusToken(1),
            new IntToken(2, 2),
            new MinusToken(3),
            new IntToken(4, 3)
        }),
        new TestCaseData("1*2/3", new Token[]
        {
            new IntToken(0, 1),
            new MultiplyToken(1),
            new IntToken(2, 2),
            new DivideToken(3),
            new IntToken(4, 3)
        }),
        new TestCaseData("+-1", new Token[]
        {
            new PlusToken(0),
            new MinusToken(1),
            new IntToken(2, 1)
        }),
    };

    public static IEnumerable<TestCaseData> GrammarConsecutiveExpressions = new[]
    {
        new TestCaseData("1*2+3*4", new Token[]
        {
            new IntToken(0, 1),
            new MultiplyToken(1),
            new IntToken(2, 2),
            new PlusToken(3),
            new IntToken(4, 3),
            new MultiplyToken(5),
            new IntToken(6, 4)
        }),
        new TestCaseData("1/2-3/4", new Token[]
        {
            new IntToken(0, 1),
            new DivideToken(1),
            new IntToken(2, 2),
            new MinusToken(3),
            new IntToken(4, 3),
            new DivideToken(5),
            new IntToken(6, 4)
        }),
        new TestCaseData("1**2*3**4", new Token[]
        {
            new IntToken(0, 1),
            new PowerToken(1),
            new IntToken(3, 2),
            new MultiplyToken(4),
            new IntToken(5, 3),
            new PowerToken(6),
            new IntToken(8, 4)
        }),
        new TestCaseData("1**2/3**4", new Token[]
        {
            new IntToken(0, 1),
            new PowerToken(1),
            new IntToken(3, 2),
            new DivideToken(4),
            new IntToken(5, 3),
            new PowerToken(6),
            new IntToken(8, 4)
        }),
        new TestCaseData("+1**-2", new Token[]
        {
            new PlusToken(0),
            new IntToken(1, 1),
            new PowerToken(2),
            new MinusToken(4),
            new IntToken(5, 2),
        }),
        new TestCaseData("+1!", new Token[]
        {
            new PlusToken(0),
            new IntToken(1, 1),
            new ExclamationToken(2),
        }),
        new TestCaseData("-1!", new Token[]
        {
            new MinusToken(0),
            new IntToken(1, 1),
            new ExclamationToken(2),
        }),
        new TestCaseData("-(1)", new Token[]
        {
            new MinusToken(0),
            new OpenParenthesisToken(1),
            new IntToken(2, 1),
            new CloseParenthesisToken(3),
        }),
        new TestCaseData("(1+2)", new Token[]
        {
            new OpenParenthesisToken(0),
            new IntToken(1, 1),
            new PlusToken(2),
            new IntToken(3, 2),
            new CloseParenthesisToken(4),
        }),
    };

    [TestCaseSource(nameof(SingleExpressions)), Category(nameof(SingleExpressions))]
    [TestCaseSource(nameof(GrammarSameLevelExpressions)), Category(nameof(GrammarSameLevelExpressions))]
    [TestCaseSource(nameof(GrammarConsecutiveExpressions)), Category(nameof(GrammarConsecutiveExpressions))]
    public void ExpressionTest(string expression, IEnumerable<Token> expectedTokens)
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