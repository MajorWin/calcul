namespace calcul {
    public abstract class Token {
    }

    public class EofToken : Token {
        private static EofToken _instance;
        public static EofToken Instance => _instance ??= new EofToken();
    }

    public class PlusToken : Token {
        private static PlusToken _instance;
        public static PlusToken Instance => _instance ??= new PlusToken();
    }

    public class MinusToken : Token {
        private static MinusToken _instance;
        public static MinusToken Instance => _instance ??= new MinusToken();
    }

    public class MultiplyToken : Token {
        private static MultiplyToken _instance;
        public static MultiplyToken Instance => _instance ??= new MultiplyToken();
    }

    public class DivideToken : Token {
        private static DivideToken _instance;
        public static DivideToken Instance => _instance ??= new DivideToken();
    }

    public class NumberToken : Token {
        public NumberToken(int value) {
            Value = value;
        }

        public int Value { get; }

        public override string ToString() => base.ToString() + ": " + Value;
    }
}