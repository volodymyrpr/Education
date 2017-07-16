using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Education._16Networking
{
    class UsingDNS
    {
        public void DoSmth()
        {
            foreach (IPAddress address in Dns.GetHostAddresses("facebook.com"))
            {
                Console.WriteLine(address.ToString());
            }

            IPHostEntry entry = Dns.GetHostEntry("157.240.20.35");
            Console.WriteLine(entry.HostName);

            IPAddress address2 = new IPAddress(new byte[] { 157, 240, 20, 35 });
            IPHostEntry entry2 = Dns.GetHostEntry(address2);
            Console.WriteLine(entry2.HostName);
        }
    }
}
