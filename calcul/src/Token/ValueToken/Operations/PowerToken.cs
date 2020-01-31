﻿namespace Calcul.Token.ValueToken.Operations
{
    public sealed class PowerToken : ValueToken<string>
    {
        public static readonly PowerToken Instance = new PowerToken();
        private PowerToken() : base("**") { }
    }
}