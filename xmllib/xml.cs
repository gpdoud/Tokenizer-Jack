using System;
using System.Xml;

namespace DSI.Xlmlib {

    public class Xml { 

        string filename = null;
        XmlDocument doc = null;

        public void Add(string token, string tokenType) {
            var element = doc.CreateElement("Token");
            var dataAttr = doc.CreateAttribute("Value");
            dataAttr.InnerText = token;
            var typeAttr = doc.CreateAttribute("Type");
            typeAttr.InnerText = tokenType;
            element.Attributes.Append(typeAttr);
            element.Attributes.Append(dataAttr);
            doc.DocumentElement.AppendChild(element);
        }

        public Xml(string filename) {
            this.filename = filename;
            doc = new XmlDocument();
            doc.LoadXml("<Tokens></Tokens>");
            doc.Save(this.filename);
        }
        public void Dispose() {
            doc.Save(this.filename);
            doc = null;
        }
    }
}