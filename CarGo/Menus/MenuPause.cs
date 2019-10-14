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
    public class MenuPause
    {
        
        private SpriteBatch spriteBatch;
        private SpriteFont spriteFont;
        private Texture2D textureBackground;
        private Texture2D textureCarrier;
        private Game1 theGame;
        private GamePadState previousState;

        private List<Vector2> buttons;
        private int stage;
        private String[] texts;

        public MenuPause(SpriteBatch spriteBatchInit, Game1 game)
        {
            spriteBatch = spriteBatchInit;
            theGame = game;
            stage = 0;

            //Boxes
            //Create Buttons 

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

        public void Update()
        {
            Input();
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

        private void Input()
        {
            if (GamePad.GetCapabilities(PlayerIndex.One).IsConnected)
            {
                GamePadState state = GamePad.GetState(PlayerIndex.One);

                if (((state.ThumbSticks.Left.Y < 0f && previousState.ThumbSticks.Left.Y == 0) || (state.IsButtonDown(Buttons.DPadDown) && previousState.IsButtonUp(Buttons.DPadDown))) && stage < 3)
                {

                    stage++;

                }
                if (((state.ThumbSticks.Left.Y > 0f && previousState.ThumbSticks.Left.Y == 0) || (state.IsButtonDown(Buttons.DPadUp) && previousState.IsButtonUp(Buttons.DPadUp))) && stage > 0)
                {

                    stage--;

                }

                if (state.IsButtonUp(Buttons.A) && previousState.IsButtonDown(Buttons.A))
                {
                    ConfirmSelection();
                }
                
                previousState = state;
            }
        }

        private void ConfirmSelection()
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

    }
}
