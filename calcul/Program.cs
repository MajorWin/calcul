using System;
using System.Globalization;
using System.Threading;
using calcul.Tokens;

namespace calcul
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            //TODO: exceptions in English
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            TestLexer();
            Console.Out.WriteLine("\n----------------------------------------\n");
            TestParser();
        }

        private static void TestLexer()
        {
            ILexer l = new ArithmeticLexer("   + 100 500 - * 600     5");

            Token t = l.Current;
            Console.Out.WriteLine(t);

            while (!(t is EofToken))
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
            ILexer l = new ArithmeticLexer("1*2/3 + 5 + 6*7");
            IParser p = new InfixToPrefixParser(l);
            foreach (var token in p.Parse())
            {
                Console.Out.Write($"{token} ");
            }
        }
    }
}