using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarGo
{
    public class LobbyOnline : Menu
    {
        private Texture2D background;
        public static List<OnlinePlayer> onlinePlayers;
        public LobbyOnline(SpriteBatch spriteBatchInit, Game1 game) : base(spriteBatchInit, game,0)
        {
            background = TextureCollection.Instance.GetTexture(TextureType.Menu_Background);
            onlinePlayers = new List<OnlinePlayer>();
        }
        protected override void ConfirmSelection()
        {
            throw new NotImplementedException();
        }

        public void Draw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);


            spriteBatch.End();
        }

        public void AddOnlinePlayer(string name, int id, InputType inputType)
        {
            onlinePlayers.Add(new OnlinePlayer(name, id, inputType));
        }

        public OnlinePlayer GetOnlinePlayer(int id)
        {
            foreach (OnlinePlayer onlinePlayer in onlinePlayers)
            {
                if(onlinePlayer.clientID==id)
                {
                    return onlinePlayer;
                }
            }
            return null;
        }

        protected override void Back()
        {
            //StateMachine.Instance.Back();
            //ToDO: Leave Lobby
        }
    }
}
