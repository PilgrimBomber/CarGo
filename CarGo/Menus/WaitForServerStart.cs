using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGo
{
    class WaitForServerStart:Menu
    {
        Network.NetworkThread networkThread;
        public WaitForServerStart(Network.NetworkThread networkThread):base(null,null,0)
        {
            this.networkThread = networkThread;
        }

        public void Update()
        {
            if (networkThread.IsServerRunning)
            {
                networkThread.ConnectToServer("localhost");
                StateMachine.Instance.ChangeState(GameState.OnlineLobby);
            }

        }

        protected override void Back(int clientID, InputController inputController)
        {
            
        }

        protected override void ConfirmSelection(int clientID, InputController inputController)
        {
            
        }
    }
}
