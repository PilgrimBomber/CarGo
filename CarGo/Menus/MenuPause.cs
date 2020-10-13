using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

namespace CarGo
{
    public class MenuPause: Menu
    {
        
        private SpriteFont spriteFont;
        private Texture2D textureBackground;
        private Texture2D textureCarrier;

        private List<Vector2> buttons;
        private String[] texts;
        private Texture2D chatWindow;
        private bool chatMode;
        private string chatMessage;
        private List<string> chatLog;
        private Keys[] lastKeys;
        public MenuPause(SpriteBatch spriteBatchInit, Game1 game): base(spriteBatchInit,game,5)
        {
            //Boxes
            //Create Buttons 
            numButtons = 5;
            buttons = new List<Vector2>();
            for (int i = 0; i < numButtons; i++)
            {
                buttons.Add(new Vector2(300, 350 + (int)i * 100));
            }

            texts = new String[numButtons];
            texts[0] = "Continue";
            texts[1] = "Settings";
            texts[2] = "Menu";
            texts[3] = "Exit";
            texts[4] = "Chat";
            lastKeys = Keyboard.GetState().GetPressedKeys();
            //Texture
            //Set Background
            HUD.graphicsDevice = spriteBatchInit.GraphicsDevice;
            chatWindow = HUD.createLifebar(chatWindow,600,400,0,2,Color.White,Color.White,Color.Black);
            chatLog = new List<string>();
            chatMessage = "";
            Color backgroundColor = new Color(0, 0, 0,100);
            textureBackground = new Texture2D(spriteBatchInit.GraphicsDevice, (int)Settings.Instance.ScreenSize.X, (int)Settings.Instance.ScreenSize.Y);
            Color[] data = new Color[(int)Settings.Instance.ScreenSize.X * (int)Settings.Instance.ScreenSize.Y];
            for(int i=0;i< (int)Settings.Instance.ScreenSize.X * (int)Settings.Instance.ScreenSize.Y; i++)
            {
                data[i] = backgroundColor;
            }
            textureBackground.SetData(data);
            textureCarrier = TextureCollection.Instance.GetTexture(TextureType.MainMenuCarrier);

            //Set font for Buttontext
            spriteFont = FontCollection.Instance.GetFont(FontCollection.Fonttyp.MainMenuButtonFont);
        }

        
        public void Update()
        {
            if(chatMode)
            {
                Keys[] keys = Keyboard.GetState().GetPressedKeys();
                foreach (Keys key in keys)
                {
                    if(!lastKeys.Contains(key))chatMessage += InputHandler.KeyToString(key,keys.Contains(Keys.LeftShift)|| keys.Contains(Keys.RightShift));
                }
                lastKeys = keys;
            }

            base.Update();
        }

        

        public void Draw()
        {
            spriteBatch.Begin();

            spriteBatch.Draw(textureBackground, new Vector2(0, 0), Color.White);

            for (int i = 0; i < numButtons; i++)
            {
                spriteBatch.DrawString(spriteFont, texts[i], buttons[i], Color.Black);
            }

            spriteBatch.Draw(textureCarrier, buttons[stage] + new Vector2(-300, -25), Color.White);

            if(chatMode)
            {
                spriteBatch.Draw(chatWindow, new Vector2(1200,500), Color.White);
                //show 10 lastMessages
                int shownMessages = 10;
                int numMessages = chatLog.Count>shownMessages? shownMessages:chatLog.Count;
                int firstIndex = chatLog.Count-numMessages;
                for (int i = 0; i < numMessages; i++)
                {
                    spriteBatch.DrawString(spriteFont, chatLog[firstIndex + i], new Vector2(1205, 505 + 30 * i), Color.Black, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0);
                }
                if(chatMessage.Length>0)spriteBatch.DrawString(spriteFont, chatMessage, new Vector2(1205, 875), Color.Black, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0);
            }


            spriteBatch.End();
        }

        

        protected override void ConfirmSelection(int clientID, InputController inputController)
        {
            switch (stage)
            {
                case 0:
                    StateMachine.Instance.ChangeState(GameState.Playing);
                    break;
                case 1:
                    StateMachine.Instance.ChangeState(GameState.MenuSettings);
                    break;
                case 2:
                    StateMachine.Instance.ChangeState(GameState.MenuMain);
                    break;
                case 3:
                    StateMachine.Instance.ChangeState(GameState.Exit);
                    break;
                case 4:
                    if(StateMachine.Instance.networkGame)
                    {
                        if (!chatMode) chatMode = true;
                        else
                        {
                            if (chatMessage.Length > 0)
                            {
                                Network.NetworkThread.Instance.BroadCastChatMessage(chatMessage);
                                AddChatMessage(chatMessage);
                            }
                            chatMessage = "";
                        }
                    }
                    break;
            }
        }

        public void AddChatMessage(string newMessage)
        {
            chatLog.Add(newMessage);
        }

        protected override void Back(int clientID, InputController inputController)
        {
            if (stage == 4 && chatMode) chatMode = false;
            else StateMachine.Instance.ChangeState(GameState.Playing);
        }


        
    }
}
