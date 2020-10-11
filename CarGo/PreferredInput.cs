using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGo
{
    public class PreferredInput
    {
        private GamePadState[] lastState;
        
        private InputController lastChanged;
        private PreferredInput()
        {
            lastState = new GamePadState[4];
        }

        public InputController GetPreferredInput
        {
            get
            {
                return lastChanged;
            }
        }

        private static PreferredInput instance;

        public static PreferredInput Instance { 
            get 
            {
                if (instance == null) instance = new PreferredInput();
                return instance;
            }
        }

        public void Update()
        {
            GamePadState state;
            for(int i = 0; i<4; i++)
            {
                if (GamePad.GetCapabilities((PlayerIndex)i).IsConnected)
                {
                    state = GamePad.GetState((PlayerIndex)i);
                    if (lastState[i] != null && state != lastState[i]) MakePreferredInput(i);
                    lastState[i] = state;
                }
            }
            if (Keyboard.GetState().GetPressedKeys().Length > 0) MakePreferredInput(4);
        }

        private void MakePreferredInput(int i)
        {
            lastChanged = (InputController)i;
        }

        
    }
}
