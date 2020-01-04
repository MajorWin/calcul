using System;
using Calcul.Tokens;

namespace Calcul
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            ILexer l = new Lexer("   + 100 500 - * 600     5");

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
    }
}