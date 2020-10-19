using System;
using System.Xml;

namespace DSI.Xlmlib {

    public enum CategoryTypes { NULL, VAR, ARGUMENT, STATIC, FIELD, CLASS, SUBROUTINE };

    public class Xml { 

        string filename = null;
        XmlDocument doc = null;

        public void Add(string token, string tokenType, CategoryTypes category = CategoryTypes.NULL, int index = -1) {
            var element = doc.CreateElement("Token");
            var dataAttr = doc.CreateAttribute("Value");
            var catAttr = doc.CreateAttribute("Category");
            var idxAttr = doc.CreateAttribute("Index");
            dataAttr.InnerText = token;
            var typeAttr = doc.CreateAttribute("Type");
            typeAttr.InnerText = tokenType;
            element.Attributes.Append(typeAttr);
            element.Attributes.Append(dataAttr);
            if(category != CategoryTypes.NULL) {
                catAttr.InnerText = category.ToString();
                element.Attributes.Append(catAttr);
            }
            if(index != -1) {
                idxAttr.InnerText = index.ToString();
                element.Attributes.Append(idxAttr);
            }
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