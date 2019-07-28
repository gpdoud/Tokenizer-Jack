using System;

namespace DSI.Tokenizer {

    class Tokenizer {

        System.Collections.Generic.List<char> operators = new System.Collections.Generic.List<char> 
            { '=', '{', '}', '[', ']', ';', '<', '>', '(', ')', '+', '-', '*', '/', '&', '|', '~' };
        System.Collections.Generic.List<char> source;
        int pos = 0;

        public string GetToken() {
            // strip spaces
            if(pos >= source.Count) return null;
            while(source[pos] == ' ' || source[pos] == '\r' || source[pos] == '\n') { pos++; }
            // check first char of token
            // if digit, token must be a number
            string token = null;
            if (operators.Contains(source[pos])) {
                token = GetOperatorToken();
            } else if(Char.IsLetterOrDigit(source[pos])|| source[pos] == '"' || source[pos] == '_') {
                token = GetObjectToken();
            }
            return token;
        }
        string GetOperatorToken() {
            return source[pos++].ToString();
        }
        string GetObjectToken() {
            System.Collections.Generic.List<char> chars = new System.Collections.Generic.List<char>();
            while(Char.IsLetterOrDigit(source[pos]) || source[pos] == '"' || source[pos] == '_') {
                chars.Add(source[pos++]); 
            }
            return new string(chars.ToArray());
        }

        public static (string, string) GetTokenType(string token) {
            switch(token) {
                case "class":
                case "constructor":
                case "function":
                case "method":
                case "field":
                case "static":
                case "var":
                case "int":
                case "char":
                case "bool":
                case "void":
                case "true":
                case "false":
                case "null":
                case "this":
                case "let":
                case "do":
                case "if":
                case "else":
                case "while":
                case "return": return (token, "Keyword");

                case "{":
                case "}":
                case "(":
                case ")":
                case "[":
                case "]":
                case ".":
                case ",":
                case ";":
                case "+":
                case "-":
                case "*":
                case "/":
                case "&":
                case "|":
                case "<":
                case ">":
                case "=":
                case "~": return (token, "Symbol");

            }
            double dummy;
            if(double.TryParse(token, out dummy)) {
                return (token, "IntegerConstant");
            } else if(token[0] == '"') {
                var strippedToken = token.Replace('"', ' ').Replace(" ", "");
                return (strippedToken, "StringConstant");
            } else {
                return (token, "Identifier");
            }
        }

        string Filename2Str(string filename) {
            try {
            string str = System.IO.File.ReadAllText(filename);
            return str;
            } catch (System.IO.FileNotFoundException ex) {
                Console.WriteLine($"EXCEPTION: {ex.Message}");
                throw;
            }

        }
        public Tokenizer(string filename) {
            var fileAsStr = Filename2Str(filename);
            this.source = new System.Collections.Generic.List<char>(fileAsStr.ToCharArray());
        }
    }
}