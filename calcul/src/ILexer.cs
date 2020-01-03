namespace calcul {
    public interface ILexer {
        Token GetCurrent();
        Token GetNext();
    }
}