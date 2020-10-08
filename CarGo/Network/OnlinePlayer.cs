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
        public InputType inputType;
        public OnlinePlayer(string name, int id, InputType inputType)
        {
            this.name = name;
            this.clientID = id;
            this.inputType = inputType;
        }
        
        
    }

    public enum InputType
    {
        Controller1,
        Controller2,
        Controller3,
        Controller4,
        KeyBoard,
        Remote,
        Local
    }
}
