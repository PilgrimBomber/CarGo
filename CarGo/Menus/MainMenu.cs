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
    public class MainMenu : Menu
    {
        private Texture2D MainMenuBackground;
        
        private SpriteFont spriteFont;
        private Texture2D carrierTexture;
        private SoundEffectInstance soundHorn;

        public MainMenu(SpriteBatch spriteBatchInit, Game1 game): base(spriteBatchInit,game,5)
        {
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

        protected override void Back(int clientID, InputController inputController)
        {
            
        }

        protected override void ConfirmSelection(int clientID, InputController inputController)
        {
            if (stage == 0)
            {
                soundHorn.Play();
                StateMachine.Instance.ChangeState(GameState.LaunchMenu);
                
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

        

    }
}

