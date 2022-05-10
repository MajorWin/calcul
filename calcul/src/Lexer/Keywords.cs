using System.Collections.Generic;

namespace Calcul.Lexer;

public static class Keywords
{
    public const string Var = "var";

    public static readonly IEnumerable<string> KeywordList = new[] { Var };
}