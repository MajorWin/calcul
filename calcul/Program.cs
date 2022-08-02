using System;
using System.Globalization;
using System.Threading;
using Calcul.Extensions;
using Calcul.Lexer;
using Calcul.Parser;
using Calcul.Tokens;

namespace Calcul;

class Program
{
    public static void Main()
    {
        //TODO: exceptions in English
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

        TestLexer();
        Console.WriteLine("\n----------------------------------------\n");
        // TestAstParser();
    }
        
    private static void TestLexer()
    {
        ILexer l = new ArithmeticLexer("1*2-3 + (5 + 6)*7");

        var t = l.Current;
        Console.WriteLine(t);

        while (t.IsNot<EofToken>())
        {
            t = l.GetNext();
            Console.WriteLine(t);

            if (t != l.Current)
            {
                Console.WriteLine($"t ({t}) != l.Current ({l.Current})");
                return;
            }
        }
    }

    private static void TestAstParser()
    {
        // const string expression = "2! / (-1! + 0!)";
        const string expression = "1*2--   -+-3 + (5 + 6)*7 + 2!!!! + (1+2*1)! + (2!)!!**(1+2)! + (1 - 1)!";
        var l = new ArithmeticLexer(expression);
        var p = new InfixToAstParser(l);
        var e = p.Parse();
        Console.WriteLine(e.ToString());
        Console.WriteLine(e.Calculate());
    }
}