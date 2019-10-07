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
    public class PostGameMenu
    {
        private Texture2D PostGameMenuBackgroundWin;
        private Texture2D PostGameMenuBackgroundLose;
        private SpriteBatch spriteBatch;
        private SpriteFont spriteFont;
        private Game1 theGame;
        private KeyboardState previousKeyBoardState;
        private GamePadState previousState;

        public PostGameMenu(SpriteBatch spriteBatchInit, Game1 game)
        {
            spriteBatch = spriteBatchInit;
            theGame = game;

            //Texture
            //Set Background 

            PostGameMenuBackgroundWin = TextureCollection.Instance.GetTexture(TextureType.Menu_VictoryScreen);
            PostGameMenuBackgroundLose = TextureCollection.Instance.GetTexture(TextureType.Menu_DefeatScreen);

            //Set font for Buttontext
            spriteFont = FontCollection.Instance.GetFont(FontCollection.Fonttyp.MainMenuButtonFont);

        }

        //Draw the Menu 
        public void Draw()
        {
            spriteBatch.Begin();

            //Draw Background and Selection
            //spriteBatch.Draw(texture, hitbox.Center - offset, null, Color.White, hitbox.RotationRad, hitbox.Offset, 1.0f, SpriteEffects.None, 0f);
            switch (theGame.GameState)
            {
                case GameState.MenuWon:
                    spriteBatch.Draw(PostGameMenuBackgroundWin, new Vector2(0, 0), Color.White);
                    spriteBatch.DrawString(spriteFont, "Mission accomplished", new Vector2(750,530), Color.Black);
                    break;
                case GameState.MenuLost:
                    spriteBatch.Draw(PostGameMenuBackgroundLose, new Vector2(0, 0), Color.White);
                    spriteBatch.DrawString(spriteFont, "Mission failed", new Vector2(800, 530), Color.Black);
                    break;
            }
            spriteBatch.End();
        }


        public void Update()
        {

            if (GamePad.GetCapabilities(PlayerIndex.One).IsConnected)
            {
                GamePadState state = GamePad.GetState(PlayerIndex.One);

                if (state.IsButtonUp(Buttons.A) && previousState.IsButtonDown(Buttons.A))
                {
                    theGame.scene.Reset();
                    theGame.modifierMenu.Reset();
                    theGame.GameState = GameState.MenuMain;
                }

                previousState = state;
            }
            else //use Keyboard
            {
                KeyboardState keyboardstate = Keyboard.GetState();

                if (keyboardstate.IsKeyUp(Keys.Enter) && previousKeyBoardState.IsKeyDown(Keys.Enter))
                {
                    theGame.scene.Reset();
                    theGame.modifierMenu.Reset();
                    theGame.GameState = GameState.MenuMain;
                }


                previousKeyBoardState = keyboardstate;
            }







        }
    }
}
