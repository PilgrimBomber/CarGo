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
        public Int64 uniqueID;
        public string serverName;
        public string localAddress;
        public string publicAddress;
        public int serverPort;
        public int numClients;
        public bool showInServerList;
        public ServerData()
        {
            numClients = 0;
        }
    }
}
