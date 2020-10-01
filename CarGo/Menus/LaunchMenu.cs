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
    public class LaunchMenu
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
        private String[] texts;

        public LaunchMenu(SpriteBatch spriteBatchInit, Game1 game)
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

            texts = new String[5];
            texts[0] = "Play";
            texts[1] = "Controls";
            texts[2] = "Settings";
            texts[3] = "Credits";
            texts[4] = "Exit";

            //Texture
            //Set Background 
            MainMenuBackground = TextureCollection.Instance.GetTexture(TextureType.MainMenuBackground);
            carrierTexture = TextureCollection.Instance.GetTexture(TextureType.MainMenuCarrier);

            //Set font for Buttontext
            spriteFont = FontCollection.Instance.GetFont(FontCollection.Fonttyp.MainMenuButtonFont);

           soundHorn = SoundCollection.Instance.GetSoundInstance(SoundType.Car_Horn);
        }

        //Draw the Menu 
        public void Draw()
        {
            spriteBatch.Begin();

            //Draw Background and Selection
            spriteBatch.Draw(MainMenuBackground, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(carrierTexture, buttons[stage] - new Vector2(buttons[stage].X, 25), Color.White);

            //Draw Button
            
            for(int j=0;j<5;j++)
            {
                spriteBatch.DrawString(spriteFont, texts[j], buttons[j], Color.Black);
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
                StateMachine.Instance.ChangeState(GameState.MenuModificationSelection);
                theGame.modifierMenu.Reset();
                theGame.scene.Reset();
                
            }

            if (stage == 1)
            {
                StateMachine.Instance.ChangeState(GameState.MenuControls);
            }

            if (stage == 2)
            {
                StateMachine.Instance.ChangeState(GameState.MenuSettings);
            }

            if (stage == 3)
            {
                StateMachine.Instance.ChangeState(GameState.CreditScreen);
            }

            if (stage == 4)
            {
                soundHorn.Play();
                StateMachine.Instance.ChangeState(GameState.Exit);
            }
        }

        private void Input()
        {
            if (GamePad.GetCapabilities(PlayerIndex.One).IsConnected)
            {
                GamePadState state = GamePad.GetState(PlayerIndex.One);

                if (((state.ThumbSticks.Left.Y < 0f && previousState.ThumbSticks.Left.Y == 0) || (state.IsButtonDown(Buttons.DPadDown) && previousState.IsButtonUp(Buttons.DPadDown))) && stage < 4)
                {
                    stage++;

                }
                if (((state.ThumbSticks.Left.Y > 0f && previousState.ThumbSticks.Left.Y == 0) || (state.IsButtonDown(Buttons.DPadUp) && previousState.IsButtonUp(Buttons.DPadUp))) && stage > 0)
                {

                    stage--;

                }

                if(state.IsButtonUp(Buttons.A)&& previousState.IsButtonDown(Buttons.A))
                {
                    ConfirmSelection();
                }

                if (state.IsButtonUp(Buttons.B) && previousState.IsButtonDown(Buttons.B))
                {
                    stage = 4;
                }

                previousState = state;
            }
        }

    }
}

