using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace XmlDocTest
{
    class Program
    {
        static void Main(string[] args)
        {
            P p = new P();
            XmlNode top = p.CreateNode(XmlNodeType.Element, "A", "");
            XmlElement a = p.CreateElement("a", "a", @"http://www.simtest.net/");
            XmlElement b = p.CreateElement("a", "b", null);
            XmlAttribute at1 = p.CreateAttribute("Name");
            at1.InnerText = "b";
            b.Attributes.InsertBefore(at1, null);
            a.InsertBefore(b, null);
            p.InsertBefore(a, null);

            p.Save(@"c:\test.xml");
        }
    }
}
