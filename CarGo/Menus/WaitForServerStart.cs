using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGo
{
    class WaitForServerStart
    {
        Network.NetworkThread networkThread;
        public WaitForServerStart(Network.NetworkThread networkThread)
        {
            this.networkThread = networkThread;
        }

        public void Update()
        {
            if (networkThread.IsServerRunning)
            {
                networkThread.host = "localhost";
                networkThread.ConnectToServer();
                StateMachine.Instance.ChangeState(GameState.OnlineLobby);
            }

        }
    }
}
