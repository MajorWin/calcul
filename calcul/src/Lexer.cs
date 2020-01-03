using System;
using System.Runtime.InteropServices;

namespace calcul {
    public class Lexer : ILexer {
        private readonly string _string;
        private int _index = 0;
        
        private Token _current = null;

        public Lexer(string s) {
            _string = s;
        }

        public Token GetCurrent() {
            return _current ??= NextToken();
        }

        public Token GetNext() {
            return _current = NextToken();
        }

        private Token NextToken() {
            while (_index < _string.Length && _string[_index] == ' ') {
                _index++;
            }

            if (_index == _string.Length) {
                return EofToken.Instance;
            }

            switch (_string[_index]) {
                case '+':
                    _index++;
                    return PlusToken.Instance;
                case '-':
                    _index++;
                    return MinusToken.Instance;
                case '*':
                    _index++;
                    return MultiplyToken.Instance;
                case '/':
                    _index++;
                    return DivideToken.Instance;
                default:
                    if (!char.IsDigit(_string[_index])) {
                        throw new Exception("!char.IsDigit(_string[_index])");
                    }

                    var numberLength = 0;
                    while (_index + numberLength < _string.Length && char.IsDigit(_string[_index + numberLength++])) ;

                    var number = int.Parse(_string.Substring(_index, numberLength));
                    _index += numberLength;
                    
                    return new NumberToken(number);
            }
        }
    }
}