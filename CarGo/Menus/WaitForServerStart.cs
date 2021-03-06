﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        private string serverName;
        private string port;
        private bool registerServer;
        private bool inputMode;
        private bool inputModePort;
        private Keys[] lastKeys;
        private Texture2D background;
        private SpriteFont spriteFont;
        private Texture2D carrierTexture;
        private Texture2D menuYes;
        private Texture2D menuNo;
        private int serverPollCounter;
        public WaitForServerStart(Network.NetworkThread networkThread, SpriteBatch spriteBatchInit):base(spriteBatchInit,null,4)
        {
            this.networkThread = networkThread;
            registerServer = false;
            lastKeys = Keyboard.GetState().GetPressedKeys();
            background = TextureCollection.Instance.GetTexture(TextureType.Menu_Background);
            carrierTexture = TextureCollection.Instance.GetTexture(TextureType.MainMenuCarrier);
            menuYes = TextureCollection.Instance.GetTexture(TextureType.Menu_Yes);
            menuNo = TextureCollection.Instance.GetTexture(TextureType.Menu_No);
            spriteFont = FontCollection.Instance.GetFont(FontCollection.Fonttyp.MainMenuButtonFont);
            serverPollCounter = 0;
            numButtons = 4;
            buttons = new List<Vector2>();
            for (int i = 0; i < numButtons; i++)
            {
                buttons.Add(new Vector2(300, 300 + (int)i * 100));
            }
            serverName = Settings.Instance.PlayerName + "sServer";
            port = "23451";
            texts = new String[numButtons];
            texts[0] = "Server Name";
            texts[1] = "Port";
            texts[2] = "Public";
            texts[3] = "Start Server";
            inputMode = false;
        }

        public void Update()
        {
            base.Update();
            if (serverName == null)
            {
                serverName = Settings.Instance.PlayerName + "sServer";
            }
            if (inputMode)
            {
                Keys[] keys = Keyboard.GetState().GetPressedKeys();
                foreach (Keys key in keys)
                {
                    if (key != Keys.LeftShift && key != Keys.RightShift)
                        if (!lastKeys.Contains(key))
                        {
                            char keyAsString = InputHandler.KeyToString(key, keys.Contains(Keys.LeftShift) || keys.Contains(Keys.RightShift));
                            if (keyAsString == ' ')
                            {
                                if (key == Keys.Back) if (serverName.Length != 0) serverName = serverName.Substring(0, serverName.Length - 1);
                            }
                            else serverName += keyAsString;
                        }
                }
                lastKeys = keys;
            }
            if (inputModePort)
            {
                Keys[] keys = Keyboard.GetState().GetPressedKeys();
                foreach (Keys key in keys)
                {
                    if (key != Keys.LeftShift && key != Keys.RightShift)
                        if (!lastKeys.Contains(key))
                        {
                            char keyAsString = InputHandler.KeyToString(key, keys.Contains(Keys.LeftShift) || keys.Contains(Keys.RightShift));
                            if (keyAsString == ' ')
                            {
                                if (key == Keys.Back) if (port.Length != 0) port = port.Substring(0, port.Length - 1);
                            }
                            else port += keyAsString;
                        }
                }
                lastKeys = keys;
            }
            if (networkThread.serverRunning)
            {
                networkThread.ConnectToServer("localhost");
                StateMachine.Instance.ChangeState(GameState.OnlineLobby);
            }
            else
            {
                if( serverPollCounter>0)
                {
                    serverPollCounter--;
                    if(serverPollCounter==0)
                    {
                        int portI;
                        if(int.TryParse(port,out portI))
                        {
                            networkThread.CheckServerRunning("localhost", portI);
                            serverPollCounter = 20;
                        }
                        
                    }
                }
            }
            
        }

        public void Draw()
        {
            spriteBatch.Begin();

            //Draw Background and Selection
            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
            if(stage==0 && inputMode) spriteBatch.Draw(carrierTexture, new Vector2(300,buttons[0].Y -25), Color.White);
            else if (stage == 1 && inputModePort) spriteBatch.Draw(carrierTexture, new Vector2(300, buttons[1].Y - 25), Color.White);
            else spriteBatch.Draw(carrierTexture, buttons[stage] - new Vector2(buttons[stage].X, 25), Color.White);

            for (int j = 0; j < numButtons; j++)
            {
                spriteBatch.DrawString(spriteFont, texts[j], buttons[j], Color.Black);
            }
            spriteBatch.DrawString(spriteFont, serverName, buttons[0]+ new Vector2(300,0), Color.Black);
            spriteBatch.DrawString(spriteFont, port, buttons[1] + new Vector2(300, 0), Color.Black);

            if (registerServer) spriteBatch.Draw(menuYes, buttons[2] + new Vector2(300,0), Color.White);
            else spriteBatch.Draw(menuNo, buttons[2] + new Vector2(300, 0), Color.White);


            spriteBatch.End();
        }


        private void LaunchServer()
        {
            if (serverName == null ||serverName.Length==0)
            {
                serverName = Settings.Instance.PlayerName + "sServer";
            }
            int portI;
            if(int.TryParse(port,out portI))
            {
                networkThread.LaunchServer(serverName, registerServer, portI);
                networkThread.isMainClient = true;
                StateMachine.Instance.networkGame = true;
                serverPollCounter = 20;
            }
            
        }

        protected override void Up(int clientID, InputController inputController)
        {
            if(!inputMode) base.Up(clientID, inputController);
        }
        protected override void Down(int clientID, InputController inputController)
        {
            if(!inputMode) base.Down(clientID, inputController);
        }

        protected override void Back(int clientID, InputController inputController)
        {
            if (stage == 0 && inputMode) inputMode = false;
            else
            {
                StateMachine.Instance.ChangeState(GameState.LaunchMenu);
            }
        }

        protected override void ConfirmSelection(int clientID, InputController inputController)
        {
            switch(stage)
            {
                case 0:
                    if(!inputMode)
                    {
                        inputMode = true;
                    }
                    else
                    {
                        inputMode = false;
                    }
                    break;
                case 1:
                    if (!inputModePort)
                    {
                        inputModePort = true;
                    }
                    else
                    {
                        inputModePort = false;
                    }
                    break;
                case 2:
                    registerServer = !registerServer;
                    break;
                case 3:
                    LaunchServer();
                    break;
            }
        }
    }
}
