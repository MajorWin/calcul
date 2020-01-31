namespace Calcul.Tokens
{
    public abstract class Token
    {
        public int Offset { get; }

        protected Token(int offset)
        {
            Offset = offset;
        }
    }
}