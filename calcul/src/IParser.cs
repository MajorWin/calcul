﻿using System.Collections.Generic;
using Calcul.Tokens;

namespace Calcul
{
    public interface IParser
    {
        IReadOnlyList<IToken> Parse();
    }
}