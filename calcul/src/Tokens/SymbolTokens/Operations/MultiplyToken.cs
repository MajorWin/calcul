﻿namespace Calcul.Tokens.SymbolTokens.Operations;

public record MultiplyToken(int Offset): Token(Offset)
{
    public override string StringRepresentation() => "*";
}