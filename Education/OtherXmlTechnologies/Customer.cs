using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Education.OtherXmlTechnologies
{
    public class Customer
    {
        public const string XmlName = "customer";
        public int? ID;
        public string FirstName, LastName;

        public Customer() { }

        public Customer(XmlReader r) { ReadXml(r); }

        public void ReadXml(XmlReader r)
        {
            if (r.MoveToAttribute("id"))
            {
                ID = r.ReadContentAsInt();
            }
            r.ReadStartElement();
            FirstName = r.ReadElementContentAsString("firstname", "");
            LastName = r.ReadElementContentAsString("lastname", "");
            r.ReadEndElement();
        }

        public void WriteXml(XmlWriter w)
        {
            if (ID.HasValue)
            {
                w.WriteAttributeString("id", "", ID.ToString());
                w.WriteElementString("firstname", FirstName);
                w.WriteElementString("lastname", LastName);
            }
        }
    }
}
