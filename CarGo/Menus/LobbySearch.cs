using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGo
{
    class LobbySearch : Menu
    {
        

        public LobbySearch(SpriteBatch spriteBatchInit, Game1 game):base(spriteBatchInit,game,0)
        {

        }

        public void Update()
        {
            Network.NetworkThread.Instance.ConnectToServer("localhost");
            StateMachine.Instance.ChangeState(GameState.OnlineLobby);
            base.Update();
        }

        protected override void Back(int clientID, InputController inputController)
        {
            throw new NotImplementedException();
        }

        protected override void ConfirmSelection(int clientID, InputController inputController)
        {
            throw new NotImplementedException();
        }

        internal void Draw()
        {
            

        }
    }
}
