using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGo
{ 
    public class OnlinePlayer
    {
        public string name;
        public int clientID;

        public OnlinePlayer(string name, int id)
        {
            this.name = name;
            this.clientID = id;
        }
        
        
    }
}
