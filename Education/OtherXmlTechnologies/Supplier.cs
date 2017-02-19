using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Education.OtherXmlTechnologies
{


    public class Supplier
    {
        public const string XmlName = "supplier";
        public string Name;

        public Supplier() { }
        public Supplier(XmlReader r) { ReadXml(r); }

        public void ReadXml(XmlReader r)
        {
            r.ReadStartElement();
            Name = r.ReadElementContentAsString("name", "");
            r.ReadEndElement();
        }

        public void WriteXml(XmlWriter w)
        {
            w.WriteElementString("name", Name);
        }
    }
}
