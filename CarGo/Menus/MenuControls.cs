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
    public class MenuControls
    {
        private Texture2D MenuControlsBackground;
        private SpriteBatch spriteBatch;
        private GamePadState previousState;
        private Game1 theGame;

        public MenuControls(SpriteBatch spriteBatchInit, Game1 game)
        {
            spriteBatch = spriteBatchInit;
            theGame = game;

            //Texture
            //Set Background 

           MenuControlsBackground = TextureCollection.Instance.GetTexture(TextureType.Menu_Controls);
        }

        //Draw the Menu 
        public void Draw()
        {
            spriteBatch.Begin();

            //Draw Background and Selection
            //spriteBatch.Draw(texture, hitbox.Center - offset, null, Color.White, hitbox.RotationRad, hitbox.Offset, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(MenuControlsBackground, new Vector2(0, 0), Color.White);

            spriteBatch.End();
        }


        public void Update()
        {
            Input();
        }

        private void Input()
        {
            if (GamePad.GetCapabilities(PlayerIndex.One).IsConnected)
            {
                GamePadState state = GamePad.GetState(PlayerIndex.One);

                if (state.IsButtonUp(Buttons.B) && previousState.IsButtonDown(Buttons.B))
                {
                    StateMachine.Instance.ChangeState(GameState.MenuMain);
                }

                previousState = state;
            }
        }
    }
}
