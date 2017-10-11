using System;
using System.Collections.Generic;

namespace Mono.WebAssembly
{
    public class HtmlElement : HtmlNode
    {
        public HtmlElement(string expr) : base(expr) {}
        public HtmlElement(int reference) : base(reference) {}

        internal static List<HtmlElement>
            ListFromReferences(string[] references)
        {
            var list = new List<HtmlElement>();

            foreach (var reference in references) {
                list.Add(new HtmlElement(Int32.Parse(reference)));
            }

            return list;
        }

        public string Id
        { 
            get { 
                return Invoke("id");
            }
        }

        public string TagName
        { 
            get { 
                return Invoke("tagName");
            }
        }

        public string ClassName
        { 
            get { 
                return Invoke("className");
            }

            set {
                Invoke("className = \"" + value + "\"");
            }
        }

        public HtmlElement Parent
        {
            get {
                return new HtmlElement(InvokeExpr("parent"));
            }
        }

        public List<HtmlElement> Children
        {
            get {
                var references = InvokeArray("children", true);

                return HtmlElement.ListFromReferences(references);
            }
        }

        public string[] AttributeNames
        {
            get {
                return InvokeArray("getAttributeNames()", false);
            }
        }

        public string GetAttribute(string name)
        {
            return Invoke("getAttribute(\"" + name + "\")");
        }

        public string InnerText
        {
            get {
                return Invoke("innerText");
            }

            set {
                Invoke("innerText = \"" + value + "\"");
            }
        }
    }
}
