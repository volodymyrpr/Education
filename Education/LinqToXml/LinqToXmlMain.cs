using Education.LinqOperators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Education.LinqToXml
{
    class LinqToXmlMain : IExecutable
    {
        NutshellContext dataContext = new NutshellContext();
        public void Execute()
        {
            NamesNamespaces();
        }

        private void XDomOverviewExample()
        {
            string xml = @"<customer id='123' status='archived'>
                            <firstname>Joe</firstname>
                            <lastname>Bloggs<!--nice name--></lastname>
                        </customer>";

            XElement customer = XElement.Parse(xml);

            XElement fromFile = XElement.Load("C:\\Users\\Володимир\\Documents\\Visual Studio 2015\\Projects\\Education\\Education\\LinqToXml\\Customers.xml");

            XElement config = XElement.Parse(
                @"<configuration>
                    <client enabled='true'>
                        <timeout>30</timeout>
                    </client>
                </configuration>");

            foreach (var child in config.Elements())
            {
                Console.WriteLine(child.Name);
            }

            XElement client = config.Element("client");

            bool enabled = (bool)client.Attribute("enabled");
            Console.WriteLine(enabled);
            client.Attribute("enabled").SetValue(!enabled);

            int timeout = (int)client.Element("timeout");
            Console.WriteLine(timeout);
            client.Element("timeout").SetValue(timeout * 2);

            client.Add(new XElement("retries", 3));
            Console.WriteLine(config);

            XElement customer2 = new XElement("customer", new XAttribute("id", 123),
                new XElement("firstname", "joe"),
                new XElement("lastname", "bloggs",
                    new XComment("nice name")));
            Console.WriteLine(customer2);

            XElement query =
                new XElement("customers",
                    from c in dataContext.Customers
                    select
                        new XElement("customer", new XAttribute("id", c.ID),
                            new XElement("firstname", c.Name,
                                new XComment("nice name")
                            )
                        )
                    );

            Console.WriteLine(query);
        }

        private void NavigatingLinqToXml()
        {
            var bench = new XElement("bench",
                new XElement("toolbox",
                    new XElement("handtool", "Hammer"),
                    new XElement("handtool", "Rasp")),
                new XElement("toolbox",
                    new XElement("handtool", "Saw"),
                    new XElement("powertool", "Nailgun")),
                new XComment("Be careful with the nailgun")
            );

            foreach (var node in bench.Nodes())
            {
                Console.WriteLine(node.ToString(SaveOptions.DisableFormatting) + ".");
            }
            Console.WriteLine();

            var elements = bench.Elements();
            foreach (var element in elements)
            {
                Console.WriteLine(element);
            }
            Console.WriteLine();

            var nailGunElement = from element in elements
                                 where element.Elements().Any(subElement => subElement.Value == "Nailgun")
                                 select element.Value;
            foreach (var element in nailGunElement)
            {
                Console.WriteLine(element);
            }
            Console.WriteLine();

            var handTools =
                from toolBox in elements
                from tool in toolBox.Elements()
                where tool.Name == "handtool"
                select tool;
            foreach (var handTool in handTools)
            {
                Console.WriteLine(handTool);
            }
            Console.WriteLine();

            int x = bench.Elements("toolbox").Count();
            Console.WriteLine(x);
            Console.WriteLine();

            var handTools2 =
                from tool in bench.Elements("toolbox").Elements("handtool")
                select tool.Value.ToUpper();
            foreach (var handTool in handTools2)
            {
                Console.WriteLine(handTool);
            }
            Console.WriteLine();

            XElement settings = XElement.Load("C:\\Users\\Володимир\\Documents\\Visual Studio 2015\\Projects\\Education\\Education\\App.config");
            var connectionStrings = settings.Element("connectionStrings");
            Console.WriteLine(connectionStrings);
            Console.WriteLine();

            Console.WriteLine(bench.Descendants("handtool").Count());
            Console.WriteLine();

            foreach (var node in bench.DescendantNodes())
            {
                Console.WriteLine(node.ToString(SaveOptions.DisableFormatting));
            }
            Console.WriteLine();

            IEnumerable<string> query =
                from c in bench.DescendantNodes().OfType<XComment>()
                where c.Value.Contains("careful")
                orderby c.Value
                select c.Value;

            foreach (var comment in query)
            {
                Console.WriteLine(comment);
            }
            Console.WriteLine();
        }

        private void UpdateNodes()
        {
            var bench = new XElement("bench",
                new XElement("toolbox",
                    new XElement("handtool", "Hammer"),
                    new XElement("handtool", "Rasp")),
                new XElement("toolbox",
                    new XElement("handtool", "Saw"),
                    new XElement("powertool", "Nailgun")),
                new XComment("Be careful with the nailgun")
            );

            var hammerContainer = bench
                .Elements()
                .Where(element => element.Elements().Where(subElement => subElement.Value == "Hammer").Count() > 0)
                .FirstOrDefault();
            //hammerContainer.Value = "replacement";
            //hammerContainer.Add(new XComment("hammer is in this toolBox"));
            //hammerContainer.AddFirst(new XComment("hammer is in this toolBox"));
            //hammerContainer.RemoveAll();
            //hammerContainer.ReplaceNodes(new XComment("replacement"));
            hammerContainer.SetElementValue("supertool", "Pokemon");
            hammerContainer.SetElementValue("supertool", "Digimon");

            hammerContainer.AddBeforeSelf(new XElement("superToolBox", new XElement("magicTool", "Magic Wand")));
            hammerContainer.Parent.FirstNode.ReplaceWith(new XComment("magic wand was here"));
            Console.WriteLine(bench);
            Console.WriteLine();

            XElement contacts = XElement.Parse(
            @"<contacts>
                <customer name='Mary'/>
                <customer name='Chris' archived='true'/>
                <supplier name='Susan'>
                    <phone archived='true'>012345678<!--confidential--></phone>
                </supplier>
            </contacts>");

            //contacts.Elements("customer").Remove();
            //contacts.Descendants()
            //    .Where(e => (bool?)e.Attribute("archived") == true)
            //    .Remove();
            var supplier = contacts.Elements()
                .Where(element => element.DescendantNodes().OfType<XComment>()
                    .Where(comment => comment.Value == "confidential").Count() > 0)
                .FirstOrDefault();

            supplier.SetValue(17.0245);
            Console.WriteLine(contacts);
            Console.WriteLine();

            var value = new XElement("three", 3.0);
            double three = (double)value;
            Console.WriteLine(three);
            Console.WriteLine();
        }

        private void SomethingElse()
        {
            var summary = new XElement("summary",
                new XText("lalala"),
                new XElement("bold", "element"),
                new XText("blablabla"));
            Console.WriteLine(summary);

            var e1 = new XElement("test1", "Hello");
            e1.Add(" World!");

            var e2 = new XElement("test2", "Hello World!");

            var e3 = new XElement("test3", "Hello");
            e3.Add(new XText(" World!"));

            Console.WriteLine(e1.Nodes().Count() + " " + e2.Nodes().Count() + " " + e3.Nodes().Count());
        }

        private void XDocument()
        {
            var styleInstruction = new XProcessingInstruction(
                "xml-stylesheet", "href='styles.css' type='text/css'");

            var docType = new XDocumentType("html",
                "-//W3C//DTD XHTML 1.0 Strict//EN",
                "http://www.w3.org/xhtml1/DTD/xhtml1-strict.dtd", null);

            XNamespace ns = "http://w3.org/1999/xhtml";
            var root =
                new XElement(ns + "html",
                    new XElement(ns + "head",
                        new XElement(ns + "title", "An XHTML page")),
                    new XElement(ns + "body",
                        new XElement(ns + "p", "This is the content")));

            var doc =
                new XDocument(
                    new XDeclaration("1.0", "utf-8", "no"),
                    new XComment("Reference a stylesheet"),
                    styleInstruction,
                    docType,
                    root);

            doc.Save("test.html");

            Console.WriteLine(doc);
            Console.WriteLine();

            Console.WriteLine(doc.Root.Name.LocalName);
            Console.WriteLine(doc.Root.Name);
            Console.WriteLine(doc.Root.Parent == null);
            foreach (var node in doc.Nodes())
            {
                Console.WriteLine(node.Parent == null);
            }
            Console.WriteLine();

            var doc2 = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("test", "data"));
            var output = new StringBuilder();
            var settings = new XmlWriterSettings { Indent = true };
            using (XmlWriter xw = XmlWriter.Create(output, settings))
            {
                doc2.Save(xw);
            }
            Console.WriteLine(output);
        }

        private void NamesNamespaces()
        {

        }
    }
}
