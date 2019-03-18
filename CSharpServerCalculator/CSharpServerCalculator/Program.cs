using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpServerCalculator.test
{
    class Program
    {
        static void Main(string[] args)
        {
            TransportIPv4 transport = new TransportIPv4();
            transport.Bind("192.168.1.4", 9999);

            GameServer server = new GameServer(transport);
            

            server.Update();
            
        }
    }
}
