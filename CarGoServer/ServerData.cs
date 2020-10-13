using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CarGoServer
{
    public class ServerData
    {
        public string serverName;
        public IPAddress localAddress;
        public string publicAddress;
        public int serverPort;
        public int numClients;

        public ServerData()
        {
            numClients = 0;
        }
    }
}
