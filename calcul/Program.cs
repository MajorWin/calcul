using System;
using System.Globalization;
using System.Threading;
using Calcul.Extensions;
using Calcul.Lexer;
using Calcul.Parser;
using Calcul.Parser.Ast;
using Calcul.Token;

namespace Calcul
{
    class Program
    {
        private static readonly string myExpressionString = "1*2-3 + 5 + 6*7"; 
        
        public static void Main(string[] args)
        {
            //TODO: exceptions in English
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            TestLexer();
            Console.Out.WriteLine("\n----------------------------------------\n");
            TestParser();
            Console.Out.WriteLine("\n----------------------------------------\n");
            TestInterpreter();
            Console.Out.WriteLine("\n----------------------------------------\n");
            TestAstParser();
        }

        private static void TestLexer()
        {
            ILexer l = new ArithmeticLexer("   + 100 500 - * 600     5");

            var t = l.Current;
            Console.Out.WriteLine(t);

            while (t.IsNot<EofToken>())
            {
                t = l.GetNext();
                Console.Out.WriteLine(t);

                if (t != l.Current)
                {
                    Console.Out.WriteLine("t != l.GetCurrent()");
                    return;
                }
            }
        }

        private static void TestParser()
        {
            var l = new ArithmeticLexer(myExpressionString);
            var p = new InfixToPostfixParser(l);
            foreach (var token in p.Parse())
            {
                Console.Out.Write($"{token} ");
            }
        }

        private static void TestInterpreter()
        {
            var l = new ArithmeticLexer(myExpressionString);
            var p = new InfixToPostfixParser(l);
            var i = new IntExpressionInterpreter(p.Parse());
            Console.Out.WriteLine(i.Interpret());
        }

        private static void TestAstParser()
        {
            var l = new ArithmeticLexer(myExpressionString);
            var p = new InfixToAstParser(l);
            Console.Out.Write(p.Parse().Calculate());
        }
    }
}