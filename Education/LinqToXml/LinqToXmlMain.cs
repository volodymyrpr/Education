using Education.LinqOperators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Education.LinqToXml
{
    class LinqToXmlMain : IExecutable
    {
        NutshellContext dataContext = new NutshellContext();
        public void Execute()
        {
            NavigatingLinqToXml();
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
            foreach(var element in elements)
            {
                Console.WriteLine(element);
            }
            Console.WriteLine();

            var nailGunElement = from element in elements
                                 where element.Elements().Any(subElement => subElement.Value == "Nailgun")
                                 select element.Value;
            foreach(var element in nailGunElement)
            {
                Console.WriteLine(element);
            }
            Console.WriteLine();
        }
    }
}
