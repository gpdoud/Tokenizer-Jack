using System;
using System.Xml;

namespace DSI.Xlmlib {

    public class Xml { 

        string filename = null;
        XmlDocument doc = new XmlDocument();

        public void Add(string token, string tokenType) {
            var element = doc.CreateElement(tokenType);
            element.InnerText = token;
            doc.DocumentElement.AppendChild(element);
        }

        public Xml(string filename) {
            this.filename = filename;
            doc.LoadXml("<Terms></Terms>");
            doc.Save(this.filename);
        }
        public void Dispose() {
            doc.Save(this.filename);
        }
    }
}