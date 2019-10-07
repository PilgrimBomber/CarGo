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
    public class MainMenu
    {
        private Texture2D MainMenuBackground;
        private SpriteBatch spriteBatch;
        private SpriteFont spriteFont;
        private Texture2D carrierTexture;
        private SoundEffectInstance soundHorn;
        private Game1 theGame;
        private GamePadState previousState;

        private List<Vector2> buttons;
        private int stage;

        public MainMenu(SpriteBatch spriteBatchInit,Vector2 screenSize, ContentManager contentManager, Game1 game)
        {
            spriteBatch = spriteBatchInit;
            theGame = game;
            stage = 0;

            //Boxes
            //Create Buttons 

            buttons = new List<Vector2>();
            for (int i = 0; i < 5; i++)
            {
                buttons.Add(new Vector2(300, 300 + (int)i * 100));
            }

            //Texture
            //Set Background 
            MainMenuBackground = TextureCollection.Instance.GetTexture(TextureType.MainMenuBackground);
            carrierTexture = TextureCollection.Instance.GetTexture(TextureType.MainMenuCarrier);

            //Set font for Buttontext 
            FontCollection.Instance.LoadFonts(contentManager);
            spriteFont = FontCollection.Instance.GetFont(FontCollection.Fonttyp.MainMenuButtonFont);

           soundHorn = SoundCollection.Instance.GetSoundInstance(SoundType.Car_Horn);
        }

        //Draw the Menu 
        public void Draw()
        {
            spriteBatch.Begin();

            //Draw Background and Selection
            spriteBatch.Draw(MainMenuBackground, new Vector2(0, 0), Color.White);
            //carrierBoxConers = carrierBox.Corners;
            spriteBatch.Draw(carrierTexture, buttons[stage] - new Vector2(buttons[stage].X, 25), Color.White);

            //Draw Button
            int j = 0;
            foreach (Vector2 vector2 in buttons)
            {
                switch (j)
                {
                    case 0:
                        spriteBatch.DrawString(spriteFont, "Play", buttons[j], Color.Black);
                        j++;
                        break;
                    case 1:
                        spriteBatch.DrawString(spriteFont, "Controls", buttons[j], Color.Black);
                        j++;
                        break;
                    case 2:
                        spriteBatch.DrawString(spriteFont, "Options", buttons[j], Color.Black);
                        j++;
                        break;
                    case 3:
                        spriteBatch.DrawString(spriteFont, "Credits", buttons[j], Color.Black);
                        j++;
                        break;
                    case 4:
                        spriteBatch.DrawString(spriteFont, "Exit", buttons[j], Color.Black);
                        j++;
                        break;

                }

            }

            spriteBatch.End();
        }
     
        public void Update()
        {
            //TODO: Gamepad Input
            //Check which Button is selected 
            this.Input();


        }

        private void ConfirmSelection()
        {
            if (stage == 0)
            {
                soundHorn.Play();
                theGame.GameState = GameState.MenuModificationSelection;
            }

            if (stage == 1)
            {
                theGame.GameState = GameState.MenuControls;
            }

            if (stage == 2)
            {
                theGame.GameState = GameState.CreditScreen;
            }

            if (stage == 3)
            {
                theGame.GameState = GameState.CreditScreen;
            }

            if (stage == 4)
            {
                soundHorn.Play();
                theGame.GameState = GameState.Exit;
            }
        }

        private void Input()
        {
            if (GamePad.GetCapabilities(PlayerIndex.One).IsConnected)
            {
                GamePadState state = GamePad.GetState(PlayerIndex.One);

                if (state.ThumbSticks.Left.Y < 0f && previousState.ThumbSticks.Left.Y == 0 && stage < 4)
                {

                    stage++;

                }
                if (state.ThumbSticks.Left.Y > 0f && previousState.ThumbSticks.Left.Y == 0 && stage > 0)
                {

                    stage--;

                }

                if(state.IsButtonUp(Buttons.A)&& previousState.IsButtonDown(Buttons.A))
                {
                    ConfirmSelection();
                }

                previousState = state;
            }
        }

    }
}

