﻿namespace Calcul.Tokens.ValueToken.OperationToken
{
    public sealed class PlusToken : OperationToken
    {
        public static readonly PlusToken Instance = new PlusToken();
        private PlusToken() : base('+') { }
    }
}