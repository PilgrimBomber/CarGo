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
    public class LaunchMenu : Menu
    {
        private Texture2D MainMenuBackground;
        
        private SpriteFont spriteFont;
        private Texture2D carrierTexture;
        private SoundEffectInstance soundHorn;
        private Network.NetworkThread networkThread;



        public LaunchMenu(SpriteBatch spriteBatchInit, Game1 game, Network.NetworkThread networkThread):base(spriteBatchInit,game,3)
        {
            this.networkThread = networkThread;
            
            
            buttons = new List<Vector2>();
            for (int i = 0; i < numButtons; i++)
            {
                buttons.Add(new Vector2(300, 300 + (int)i * 100));
            }

            texts = new String[numButtons];
            texts[0] = "Local Game";
            texts[1] = "Host Game";
            texts[2] = "Join Game";

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
            
            for(int j=0;j<numButtons;j++)
            {
                spriteBatch.DrawString(spriteFont, texts[j], buttons[j], Color.Black);
            }

            spriteBatch.End();
        }

        protected override void Back(int clientID, InputController inputController)
        {
            StateMachine.Instance.ChangeState(GameState.MenuMain);
        }

        protected override void ConfirmSelection(int clientID, InputController inputController)
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
                StateMachine.Instance.ChangeState(GameState.WaitForServerStart);
                StateMachine.Instance.networkGame = true;
                theGame.scene.Reset();
            }

            if (stage == 2)
            {
                
                StateMachine.Instance.networkGame = true;
                StateMachine.Instance.ChangeState(GameState.SearchLobby);
                theGame.scene.Reset();
                networkThread.RequestServerList();
            }
        }

        
    }
}

