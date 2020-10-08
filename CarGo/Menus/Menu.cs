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



        public Menu(SpriteBatch spriteBatchInit, Game1 game, int numButtons)
        {
            this.numButtons = numButtons;
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

        protected abstract void Back();

        protected virtual void Left()
        {

        }
        protected virtual void Right()
        {

        }

        protected virtual void Up()
        {
            if (stage > 0) stage--;
        }

        protected virtual void Down()
        {
            if (stage < numButtons - 1) stage++;
        }
        protected void GamepadInput()
        {
            if (GamePad.GetCapabilities(PlayerIndex.One).IsConnected)
            {
                GamePadState state = GamePad.GetState(PlayerIndex.One);

                if (((state.ThumbSticks.Left.Y < 0f && previousState.ThumbSticks.Left.Y == 0) || (state.IsButtonDown(Buttons.DPadDown) && previousState.IsButtonUp(Buttons.DPadDown))) && stage < numButtons - 1)
                {
                    Input(InputType.Down);

                }
                if (((state.ThumbSticks.Left.Y > 0f && previousState.ThumbSticks.Left.Y == 0) || (state.IsButtonDown(Buttons.DPadUp) && previousState.IsButtonUp(Buttons.DPadUp))) && stage > 0)
                {

                    Input(InputType.Up);

                }

                if (state.IsButtonUp(Buttons.A) && previousState.IsButtonDown(Buttons.A))
                {
                    Input(InputType.Confirm);
                }

                if (state.IsButtonUp(Buttons.B) && previousState.IsButtonDown(Buttons.B))
                {
                    Input(InputType.Back);
                }

                if ((state.ThumbSticks.Left.X > 0.2f && previousState.ThumbSticks.Left.X < 0.2f) || (state.IsButtonDown(Buttons.DPadRight) && previousState.IsButtonUp(Buttons.DPadRight)))
                {
                    Input(InputType.Left);
                }
                if ((state.ThumbSticks.Left.X < -0.2f && previousState.ThumbSticks.Left.X > -0.2f) || (state.IsButtonDown(Buttons.DPadLeft) && previousState.IsButtonUp(Buttons.DPadLeft)))
                {
                    Input(InputType.Right);
                }
                previousState = state;
            }
        }

        protected void KeyBoardInput()
        {
            KeyboardState state = Keyboard.GetState();
            if (((state.IsKeyDown(Keys.Down) && previousKeyBoardState.IsKeyUp(Keys.Down)) || (state.IsKeyDown(Keys.S) && previousKeyBoardState.IsKeyUp(Keys.S))) && stage < numButtons - 1)
            {
                Input(InputType.Down);

            }
            if (((state.IsKeyDown(Keys.Up) && previousKeyBoardState.IsKeyUp(Keys.Up)) || (state.IsKeyDown(Keys.W) && previousKeyBoardState.IsKeyUp(Keys.W))) && stage > 0)
            {
                Input(InputType.Up);
            }

            if (state.IsKeyUp(Keys.Enter) && previousKeyBoardState.IsKeyDown(Keys.Enter))
            {
                Input(InputType.Confirm);
            }

            if (state.IsKeyUp(Keys.Back) && previousKeyBoardState.IsKeyDown(Keys.Back))
            {
                Input(InputType.Back);
            }

            previousKeyBoardState = state;
        }

        

        public void Input(InputType remoteInputType)
        {
            switch (remoteInputType)
            {
                case InputType.Up:
                    Up();
                    break;
                case InputType.Down:
                    Down();
                    break;
                case InputType.Confirm:
                    ConfirmSelection();
                    break;
                case InputType.Back:
                    Back();
                    stage = 0;
                    break;
                case InputType.Left:
                    Left();
                    break;
                case InputType.Right:
                    Right();
                    break;
            }
        }

    }


    public enum InputType
    {
        Up,
        Down,
        Left,
        Right,
        Confirm,
        Back
    }
}
