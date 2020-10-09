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

        public MenuPause(SpriteBatch spriteBatchInit, Game1 game): base(spriteBatchInit,game,4)
        {
            //Boxes
            //Create Buttons 
            numButtons = 4;
            buttons = new List<Vector2>();
            for (int i = 0; i < 4; i++)
            {
                buttons.Add(new Vector2(300, 350 + (int)i * 100));
            }

            texts = new String[4];
            texts[0] = "Continue";
            texts[1] = "Settings";
            texts[2] = "Menu";
            texts[3] = "Exit";

            //Texture
            //Set Background
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

        

        public void Draw()
        {
            spriteBatch.Begin();

            spriteBatch.Draw(textureBackground, new Vector2(0, 0), Color.White);

            for (int i = 0; i < 4; i++)
            {
                spriteBatch.DrawString(spriteFont, texts[i], buttons[i], Color.Black);
            }

            spriteBatch.Draw(textureCarrier, buttons[stage] + new Vector2(-300, -25), Color.White);

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

            }
        }

        protected override void Back(int clientID, InputController inputController)
        {
            StateMachine.Instance.ChangeState(GameState.Playing);
        }
    }
}
