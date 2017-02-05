using Education.LinqOperators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Education.OtherXmlTechnologies
{
    class OtherXmlTechnologiesMain : IExecutable
    {
        NutshellContext dataContext = new NutshellContext();
        public void Execute()
        {
            XmlReaderExample();
        }

        private void XmlReaderExample()
        {
            var settings = new XmlReaderSettings()
            {
                DtdProcessing = DtdProcessing.Parse,
                IgnoreWhitespace = true
            };

            using (var reader = XmlReader.Create(
                "C:\\Users\\Володимир\\Documents\\Visual Studio 2015\\Projects\\Education\\Education\\OtherXmlTechnologies\\Customers.xml",
                settings))
            {
                //while (reader.Read())
                //{
                //    Console.Write(reader.NodeType.ToString().PadRight(17, '-'));
                //    Console.Write("> ".PadRight(reader.Depth * 3));

                //    switch(reader.NodeType)
                //    {
                //        case XmlNodeType.Element:
                //        case XmlNodeType.EndElement:
                //            Console.WriteLine(reader.Name);
                //            break;
                //        case XmlNodeType.Text:
                //        case XmlNodeType.CDATA:
                //        case XmlNodeType.Comment:
                //        case XmlNodeType.XmlDeclaration:
                //            Console.WriteLine(reader.Value);
                //            break;
                //        case XmlNodeType.DocumentType:
                //            Console.WriteLine(reader.Name + " - " + reader.Value);
                //            break;
                //        default:
                //            break;
                //    }

                //}

                //Console.WriteLine(reader.Name);
                //reader.ReadStartElement("firstname");
                //Console.WriteLine(reader.Value);
                //reader.Read();
                //Console.WriteLine(reader.Name);
                //reader.ReadEndElement();

                reader.Read();
                //Console.WriteLine(reader["id"]);
                //Console.WriteLine(reader["status"]);

                //reader.MoveToAttribute("status");
                //Console.WriteLine(reader.ReadContentAsString());
                //reader.MoveToAttribute("id");
                //Console.WriteLine(reader.ReadContentAsInt());

                if (reader.MoveToFirstAttribute())
                {
                    do
                    {
                        Console.WriteLine(reader.Name + "=" + reader.Value);
                    }
                    while (reader.MoveToNextAttribute());
                }

                reader.ReadStartElement();

                string firstName = reader.ReadElementContentAsString("firstname", "");
                string lastName = reader.Name != "lastname"
                    ? null
                    : reader.ReadElementContentAsString("lastname", "");
                decimal? averagePrice = reader.ReadElementContentAsDecimal();
                int? age = reader.ReadElementContentAsInt();

                bool isEmpty = reader.IsEmptyElement;
                reader.ReadStartElement("emptyelement");
                if (!isEmpty)
                {
                    reader.ReadEndElement();
                }
                Console.WriteLine(firstName);
                Console.WriteLine(lastName);
                Console.WriteLine(averagePrice);
                Console.WriteLine(age);
            }
        }
    }
}