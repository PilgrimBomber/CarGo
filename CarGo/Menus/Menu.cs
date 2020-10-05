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
    public abstract class Menu
    {
        protected GamePadState previousState;
        protected KeyboardState previousKeyBoardState;
        protected SpriteBatch spriteBatch;
        protected Game1 theGame;
        protected int numButtons;
        protected List<Vector2> buttons;
        protected int stage;
        protected String[] texts;



        public Menu(SpriteBatch spriteBatchInit, Game1 game)
        {
            spriteBatch = spriteBatchInit;
            theGame = game;
            stage = 0;
        }

        public void Update()
        {
            this.GamepadInput();
            this.KeyBoardInput();
        }

        protected abstract void ConfirmSelection();
        protected void GamepadInput()
        {
            if (GamePad.GetCapabilities(PlayerIndex.One).IsConnected)
            {
                GamePadState state = GamePad.GetState(PlayerIndex.One);

                if (((state.ThumbSticks.Left.Y < 0f && previousState.ThumbSticks.Left.Y == 0) || (state.IsButtonDown(Buttons.DPadDown) && previousState.IsButtonUp(Buttons.DPadDown))) && stage < numButtons - 1)
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

                if (state.IsButtonUp(Buttons.B) && previousState.IsButtonDown(Buttons.B))
                {
                    stage = numButtons - 1;
                }

                previousState = state;
            }
        }

        protected void KeyBoardInput()
        {
            KeyboardState state = Keyboard.GetState();
            if (((state.IsKeyDown(Keys.Right) && previousKeyBoardState.IsKeyUp(Keys.Right)) || (state.IsKeyDown(Keys.D) && previousKeyBoardState.IsKeyUp(Keys.D))) && stage < numButtons - 1)
            {
                stage++;

            }
            if (((state.IsKeyDown(Keys.Left) && previousKeyBoardState.IsKeyUp(Keys.Left)) || (state.IsKeyDown(Keys.A) && previousKeyBoardState.IsKeyUp(Keys.A))) && stage > 0)
            {
                stage--;
            }

            if (state.IsKeyUp(Keys.Enter) && previousKeyBoardState.IsKeyDown(Keys.Enter))
            {
                ConfirmSelection();
            }

            if (state.IsKeyUp(Keys.Back) && previousKeyBoardState.IsKeyDown(Keys.Back))
            {
                stage = numButtons - 1;
            }

            previousKeyBoardState = state;
        }
        
    }
}
