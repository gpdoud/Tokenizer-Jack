using System;
using DSI.JsonLib;
using DSI.Xlmlib;

namespace DSI.Tokenizer {

    class Program {
    
        static void Main(string[] args) {
            var xdoc = new Xml("InFile.xml");
            var tokenizer = new Tokenizer("./inFile.jack");
            string token = null;
            while((token = tokenizer.GetToken()) != null) {
                Console.WriteLine(token);
                var (newtoken, type) = Tokenizer.GetTokenType(token);
                xdoc.Add(newtoken, type);
            }
            xdoc.Dispose();
        }
    }
}