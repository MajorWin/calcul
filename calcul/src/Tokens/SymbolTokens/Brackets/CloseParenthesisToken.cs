﻿namespace Calcul.Tokens.SymbolTokens.Brackets;

public record CloseParenthesisToken(int Offset): Token(Offset)
{
    public override string StringRepresentation() => ")";
}