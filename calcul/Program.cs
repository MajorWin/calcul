using System;
using System.Globalization;
using System.Threading;
using Calcul.Tokens;

namespace Calcul
{
    class Program
    {
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
            ILexer l = new ArithmeticLexer("1*2-3 + 5 + 6*7");
            IParser p = new InfixToPrefixParser(l);
            foreach (var token in p.Parse())
            {
                Console.Out.Write($"{token} ");
            }
        }

        private static void TestInterpreter()
        {
            ILexer l = new ArithmeticLexer("1*2-3 + 5 + 6*7");
            IParser p = new InfixToPrefixParser(l);
            IntExpressionInterpreter i = new IntExpressionInterpreter(p.Parse());
            Console.Out.WriteLine(i.Interpret());
        }
    }
}