﻿namespace Calcul.Tokens.SymbolTokens.Operations;

public record DivideToken(int Offset): Token(Offset)
{
    public override string StringRepresentation() => "/";
}