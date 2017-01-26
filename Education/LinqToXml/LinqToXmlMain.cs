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
        public void Execute()
        {
            
        }

        private void XDomOverviewExample()
        {
            string xml = @"<customer id='123' status='archived'>
                            <firstname>Joe</firstname>
                            <lastname>Bloggs<!--nice name--></lastname>
                        </customer>";

            XElement customer = XElement.Parse(xml);
        }
    }
}
