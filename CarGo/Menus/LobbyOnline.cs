﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;

namespace CarGo
{
    public class LobbyOnline : Menu
    {
        private Texture2D background;
        private Texture2D carrierTexture;
        public static List<OnlinePlayer> onlinePlayers;
        //public string serverName;
        //public string serverAddress;
        public CarGoServer.ServerData serverData;
        private Vector2[] namePositions;
        private SpriteFont spriteFont;

        private Texture2D playerBox;
        private Texture2D menuYes;
        private Texture2D menuNo;
        public LobbyOnline(SpriteBatch spriteBatchInit, Game1 game) : base(spriteBatchInit, game,3)
        {
            background = TextureCollection.Instance.GetTexture(TextureType.Menu_Background);
            carrierTexture = TextureCollection.Instance.GetTexture(TextureType.MainMenuCarrier);
            HUD.graphicsDevice = spriteBatchInit.GraphicsDevice;
            playerBox = HUD.createLifebar(playerBox, 600, 400, 0, 2, Color.Transparent, Color.Transparent, Color.Black);
            menuYes = TextureCollection.Instance.GetTexture(TextureType.Menu_Yes);
            menuNo = TextureCollection.Instance.GetTexture(TextureType.Menu_No);
            onlinePlayers = new List<OnlinePlayer>();

            namePositions = new Vector2[4];
            for (int i = 0; i < 4; i++)
            {
                namePositions[i].X = 900;
                namePositions[i].Y = 300 + i*100;
            }

            buttons = new List<Vector2>();
            buttons.Add(new Vector2(200, 300));
            buttons.Add(new Vector2(200, 400));
            buttons.Add(new Vector2(200, 500));
            buttons.Add(new Vector2(200, 600));

            spriteFont = FontCollection.Instance.GetFont(FontCollection.Fonttyp.MainMenuButtonFont);

            texts = new string[3];
            texts[0] = "Ready";
            
            texts[1] = "Copy ServerAddress";
            texts[2] = "Copy InviteCode";

        }
        protected override void ConfirmSelection(int clientID, InputController inputController)
        {
            switch (stage)
            {
                case 0:
                    if (clientID == ID_Manager.Instance.ClientNumber);
                    IdentifyOnlinePlayer(clientID).ToggleReady();
                    Network.NetworkThread.Instance.BroadCastReady();
                    break;
                case 1:
                    CopyServerAddressToClipboard();
                    break;
                case 2:
                    if(Network.NetworkThread.Instance.publicServer) CopyInviteCodeToClipboard();
                    break;
            }
        }

        private void CopyInviteCodeToClipboard()
        {
            Clipboard.SetData(DataFormats.Text, (object)serverData.uniqueID.ToString());
        }
        private void CopyServerAddressToClipboard()
        {
            Clipboard.SetData(DataFormats.Text, (object)serverData.publicAddress);
        }

        private int IdentifyOnlinePlayerIndex(int clientID)
        {
            
            for (int i = 0; i < onlinePlayers.Count; i++)
            {
                if (onlinePlayers[i].clientID == clientID) return i;

            }
            return -1;
        }
        private OnlinePlayer IdentifyOnlinePlayer(int clientID)
        {

            for (int i = 0; i < onlinePlayers.Count; i++)
            {
                if (onlinePlayers[i].clientID == clientID) return onlinePlayers[i];

            }
            return null;
        }

        public void SetPlayerReady(int clientID)
        {
            IdentifyOnlinePlayer(clientID).ToggleReady();
        }

        public void Update()
        {
            if(CheckReady()) StateMachine.Instance.ChangeState(GameState.MenuModificationSelection);


            base.Update();
        }

        public void Draw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);

            if(serverData!=null)spriteBatch.DrawString(spriteFont,"Server Name: " + serverData.serverName , new Vector2(200,100), Color.Black);

            spriteBatch.Draw(playerBox, namePositions[0] - new Vector2(25, 25),Color.White);
            for (int j = 0; j < onlinePlayers.Count; j++)
            {
                spriteBatch.DrawString(spriteFont, onlinePlayers[j].name, namePositions[j], Color.Black);
                //Draw ready/not ready
                if (onlinePlayers[j].ready) spriteBatch.Draw(menuYes, namePositions[j] + new Vector2(500, 0), Color.White);
                else spriteBatch.Draw(menuNo, namePositions[j] + new Vector2(500, 0), Color.White);


            }


            spriteBatch.Draw(carrierTexture, buttons[stage] - new Vector2(carrierTexture.Width, 25), Color.White);
            for (int j = 0; j < (Network.NetworkThread.Instance.publicServer ? numButtons:numButtons-1); j++)
            {
                spriteBatch.DrawString(spriteFont, texts[j], buttons[j], Color.Black);
            }

            spriteBatch.End();
        }

        public void AddOnlinePlayer(string name, int id, InputController inputType)
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

        private bool CheckReady()
        {
            bool ret = true;
            if (onlinePlayers.Count == 0) return false;
            foreach (OnlinePlayer onlinePlayer in onlinePlayers)
            {
                if (!onlinePlayer.ready) ret = false;
            }
            return ret;
        }

        protected override void Back(int clientID, InputController inputController)
        {
            //StateMachine.Instance.Back();
            //ToDO: Leave Lobby
        }

    }
}
