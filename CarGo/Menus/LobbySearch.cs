using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CarGo
{
    public class LobbySearch : Menu
    {
        //ToDo: periodisch neue Liste von Hosts anfordern

        private Texture2D background;
        private Texture2D carrierTexture;
        private SpriteFont spriteFont;

        private Texture2D serverBox;
        private string inputIP;
        private int iPInputMode;
        private string inputCode;
        private string inputPort;
        private int codeInputMode;
        private List<CarGoServer.ServerData> serverDatas;
        private int serverSelectionMode;
        private int serverSelectedIndex;
        private Keys[] lastKeys;

        public LobbySearch(SpriteBatch spriteBatchInit, Game1 game):base(spriteBatchInit,game,3)
        {
            background = TextureCollection.Instance.GetTexture(TextureType.Menu_Background);
            carrierTexture = TextureCollection.Instance.GetTexture(TextureType.MainMenuCarrier);
            spriteFont = FontCollection.Instance.GetFont(FontCollection.Fonttyp.MainMenuButtonFont);
            HUD.graphicsDevice = spriteBatchInit.GraphicsDevice;            
            serverBox = HUD.createLifebar(serverBox, 600, 400, 0, 2, Color.Transparent, Color.Transparent, Color.Black);
            serverDatas = new List<CarGoServer.ServerData>();
            serverSelectedIndex = 0;
            iPInputMode = 0;
            codeInputMode = 0;
            lastKeys = Keyboard.GetState().GetPressedKeys();
            inputIP = "localhost";
            inputPort = "23451";
            inputCode = "";
            buttons = new List<Vector2>();
            buttons.Add(new Vector2(200, 300));
            buttons.Add(new Vector2(200, 400));
            buttons.Add(new Vector2(200, 550));
            texts = new string[]{
            "Join with IP",
            "Join with InviteCode",
            "Select from List"
            }
            ;
            

        }

        
        public void Update(GameTime gameTime)
        {
            if (iPInputMode>=1 && iPInputMode<=2)
            {

                Keys[] keys = Keyboard.GetState().GetPressedKeys();
                if(keys.Contains(Keys.LeftControl)&& keys.Contains(Keys.V))
                {
                    PasteIP();
                }
                foreach (Keys key in keys)
                {
                    if (key != Keys.LeftShift && key != Keys.RightShift)
                        if (!lastKeys.Contains(key))
                        {
                            if (iPInputMode == 1)
                            {
                                char keyAsString = InputHandler.KeyToString(key, keys.Contains(Keys.LeftShift) || keys.Contains(Keys.RightShift));
                                if (keyAsString == ' ')
                                {
                                    if (key == Keys.Back) if (inputIP.Length != 0) inputIP = inputIP.Substring(0, inputIP.Length - 1);
                                }
                                else inputIP += keyAsString;
                            }
                            if (iPInputMode == 2)
                            {
                                char keyAsString = InputHandler.KeyToString(key, keys.Contains(Keys.LeftShift) || keys.Contains(Keys.RightShift));
                                if (keyAsString == ' ')
                                {
                                    if (key == Keys.Back) if (inputPort.Length != 0) inputPort = inputPort.Substring(0, inputPort.Length - 1);
                                }
                                else inputPort += keyAsString;
                            }
                        }
                }
                lastKeys = keys;
            }
            if (codeInputMode==1)
            {
                Keys[] keys = Keyboard.GetState().GetPressedKeys();
                if (keys.Contains(Keys.LeftControl) && keys.Contains(Keys.V))
                {
                    PasteCode();
                }
                foreach (Keys key in keys)
                {
                    if (key != Keys.LeftShift && key != Keys.RightShift)
                        if (!lastKeys.Contains(key))
                        {
                            char keyAsString = InputHandler.KeyToString(key, keys.Contains(Keys.LeftShift) || keys.Contains(Keys.RightShift));
                            if (keyAsString == ' ')
                            {
                                if (key == Keys.Back) if (inputIP.Length != 0) inputIP = inputIP.Substring(0, inputIP.Length - 1);
                            }
                            else inputIP += keyAsString;
                        }
                }
                lastKeys = keys;
            }
            base.Update();
        }

        public void Draw()
        {
            spriteBatch.Begin();

            //Draw Background and Selection
            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
            if(stage==0)
            { 
                if(iPInputMode==0)spriteBatch.Draw(carrierTexture, new Vector2(0,buttons[0].Y - 25), Color.White);
                if(iPInputMode==1) spriteBatch.Draw(carrierTexture, new Vector2(400, buttons[0].Y - 25), Color.White);
                if (iPInputMode == 2) spriteBatch.Draw(carrierTexture, new Vector2(800, buttons[0].Y - 25), Color.White);
                if (iPInputMode == 3) spriteBatch.Draw(carrierTexture, new Vector2(1200, buttons[0].Y - 25), Color.White);
            }else if (stage == 1)
            {
                if (codeInputMode == 0) spriteBatch.Draw(carrierTexture, new Vector2(0, buttons[1].Y - 25), Color.White);
                if (codeInputMode == 1) spriteBatch.Draw(carrierTexture, new Vector2(400, buttons[1].Y - 25), Color.White);
                if (codeInputMode == 2) spriteBatch.Draw(carrierTexture, new Vector2(1200, buttons[1].Y - 25), Color.White);
            }else if(stage==2)
            {
                if (serverSelectionMode == 0) spriteBatch.Draw(carrierTexture, new Vector2(0, buttons[2].Y - 25), Color.White);
                if (serverSelectionMode == 1) spriteBatch.Draw(carrierTexture, new Vector2(400, buttons[2].Y - 5 + serverSelectedIndex*80), Color.White);
            }


            //Draw Button
            for (int j = 0; j < numButtons; j++)
            {
                spriteBatch.DrawString(spriteFont, texts[j], buttons[j], Color.Black);
            }
            spriteBatch.DrawString(spriteFont, "IP: "+inputIP, buttons[0]+new Vector2(400,0), Color.Black);
            spriteBatch.DrawString(spriteFont, "Port: "+inputPort, buttons[0] + new Vector2(800, 0), Color.Black);
            spriteBatch.DrawString(spriteFont, "Confirm ", buttons[0] + new Vector2(1200, 0), Color.Black);
            spriteBatch.DrawString(spriteFont, "Code: "+ inputCode, buttons[1] + new Vector2(400, 0), Color.Black);
            spriteBatch.DrawString(spriteFont, "Confirm ", buttons[1] + new Vector2(1200, 0), Color.Black);
            spriteBatch.Draw(serverBox, buttons[2] + new Vector2(400,0), Color.White);
            for (int i = 0; i < serverDatas.Count; i++)
            {
                spriteBatch.DrawString(spriteFont, serverDatas[i].serverName, buttons[2]+new Vector2(420,20+i*80),Color.Black,0,Vector2.Zero,0.7f,SpriteEffects.None,0);
                spriteBatch.DrawString(spriteFont, serverDatas[i].numClients.ToString()+"/4", buttons[2] + new Vector2(960, 20 + i * 80), Color.Black, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 0);
            }

            spriteBatch.End();
        }
        public void AddServerData(CarGoServer.ServerData serverData)
        {
            serverDatas.Add(serverData);
        }
        private void PasteIP()
        {
            string text = System.Windows.Forms.Clipboard.GetText();
            inputIP = text;
        }

        private void PasteCode()
        {
            string text = System.Windows.Forms.Clipboard.GetText();
            inputCode = text;
        }

        

        protected override void Up(int clientID, InputController inputController)
        {
            if (iPInputMode == 0 && codeInputMode == 0 && serverSelectionMode==0) base.Up(clientID, inputController);
            if(serverSelectionMode==1)
            {
                if (serverSelectedIndex > 0) serverSelectedIndex--;
            }
        }

        protected override void Down(int clientID, InputController inputController)
        {
            if (iPInputMode == 0 && codeInputMode == 0 && serverSelectionMode == 0) base.Down(clientID, inputController);
            if (serverSelectionMode == 1)
            {
                if (serverSelectedIndex < serverDatas.Count-1) serverSelectedIndex++;
            }
        }

        protected override void Back(int clientID, InputController inputController)
        {
            switch(stage)
            {
                case 0: 
                    if (iPInputMode > 0) iPInputMode--;
                    else StateMachine.Instance.ChangeState(GameState.LaunchMenu);
                    break;
                case 1:
                    if (codeInputMode > 0) codeInputMode--;
                    else StateMachine.Instance.ChangeState(GameState.LaunchMenu);
                    break;
                case 2:
                    if (serverSelectionMode>0) serverSelectionMode--;
                    else StateMachine.Instance.ChangeState(GameState.LaunchMenu);
                    break;
            }
        }

        

        protected override void ConfirmSelection(int clientID, InputController inputController)
        {
            switch (stage)
            {
                case 0:
                    if(iPInputMode==0)
                    {
                        iPInputMode = 1;
                    }
                    else if(iPInputMode==1)
                    {
                        iPInputMode = 2;
                    }
                    else if (iPInputMode == 2)
                    {
                        iPInputMode = 3;
                    }
                    else if(iPInputMode==3)
                    {
                        iPInputMode = 0;
                        IPAddress del;
                        int port=0;
                        if((IPAddress.TryParse(inputIP,out del) || inputIP == "localhost") && int.TryParse(inputPort, out port))
                        {
                            Network.NetworkThread.Instance.ConnectToServer(inputIP,port);
                            StateMachine.Instance.ChangeState(GameState.OnlineLobby);
                        }
                        else
                        {
                            Console.WriteLine("ip oder port ungültih");
                            //ip oder Port nicht gültig
                        }
                        
                    }
                    break;
                case 1:
                    if(codeInputMode==0)
                    {
                        codeInputMode = 1;
                    }
                    else if(codeInputMode==1)
                    {
                        codeInputMode = 0;
                        long code;
                        if (long.TryParse(inputCode, out code))
                        {
                            //Network.NetworkThread.Instance.RequestServerAddress(code);
                            foreach (var serverData in serverDatas)
                            {
                                if(serverData.uniqueID==code)
                                {
                                    Network.NetworkThread.Instance.ConnectToServer(serverData.localAddress, serverData.serverPort);
                                    StateMachine.Instance.ChangeState(GameState.OnlineLobby);
                                }
                            }
                        }

                    }
                    break;
                case 2:
                    if (serverSelectionMode==0)
                    {
                        serverSelectionMode = 1;
                    }
                    else if(serverSelectionMode==1)
                    {
                        serverSelectionMode = 2;
                        
                    }
                    else
                    {
                        Network.NetworkThread.Instance.ConnectToServer(serverDatas[serverSelectedIndex].localAddress, serverDatas[serverSelectedIndex].serverPort);
                        StateMachine.Instance.ChangeState(GameState.OnlineLobby);

                        //connect to selected server
                    }
                    break;

            }
        }

        
    }
}
