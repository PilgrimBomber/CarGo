﻿using System;
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
        public InputController inputType;
        public bool ready;
        
        public OnlinePlayer(string name, int id, InputController inputType)
        {
            this.name = name;
            this.clientID = id;
            this.inputType = inputType;
        }
        
        public void ToggleReady()
        {
            ready = !ready;
        }
    }

    public enum InputController
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
