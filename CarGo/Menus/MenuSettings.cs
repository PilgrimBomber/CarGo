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
    public class MenuSettings
    {
        private Texture2D settingsBackground;
        private SpriteBatch spriteBatch;
        private SpriteFont spriteFont;
        private Game1 theGame;
        private List<Vector2> positions;
        private GamePadState previousState;
        private Texture2D volumeSoundBar;
        private Texture2D volumeMusicBar;

        public MenuSettings(SpriteBatch spriteBatchInit, Game1 game)
        {
            spriteBatch = spriteBatchInit;
            theGame = game;
            settingsBackground = TextureCollection.Instance.GetTexture(TextureType.MainMenuBackground);
            spriteFont = FontCollection.Instance.GetFont(FontCollection.Fonttyp.MainMenuButtonFont);



        }


        public void Draw()
        {
            spriteBatch.Begin();

            spriteBatch.Draw(settingsBackground, new Vector2(0, 0), Color.White);

            

            spriteBatch.End();
        }


        public void Update()
        {

            this.Input();

        }


        private void Input()
        {
            if (GamePad.GetCapabilities(PlayerIndex.One).IsConnected)
            {
                GamePadState state = GamePad.GetState(PlayerIndex.One);











                if (state.IsButtonUp(Buttons.B) && previousState.IsButtonDown(Buttons.B))
                {
                    theGame.GameState = GameState.MenuMain;
                }

                previousState = state;
            }
        }



        //when changing SoundVolume call UpdateVolume() for all Entities
    }
}
