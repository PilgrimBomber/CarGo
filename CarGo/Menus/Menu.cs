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
        protected GamePadState[] previousState;
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
            previousState = new GamePadState[4];
            spriteBatch = spriteBatchInit;
            theGame = game;
            stage = 0;
        }

        public void Update()
        {
            this.GamepadInput();
            this.KeyBoardInput();
        }

        protected abstract void ConfirmSelection(int clientID, InputController inputController);

        protected abstract void Back(int clientID, InputController inputController);

        protected virtual void Left(int clientID, InputController inputController)
        {

        }
        protected virtual void Right(int clientID, InputController inputController)
        {

        }

        protected virtual void Up(int clientID, InputController inputController)
        {
            if (stage > 0) stage--;
        }

        protected virtual void Down(int clientID, InputController inputController)
        {
            if (stage < numButtons - 1) stage++;
        }
        protected void GamepadInput()
        {
            for (PlayerIndex index = PlayerIndex.One; index <= PlayerIndex.Four; index++)
            {
                if (GamePad.GetCapabilities(index).IsConnected)
                {
                    GamePadState state = GamePad.GetState(index);

                    if (((state.ThumbSticks.Left.Y < 0f && previousState[(int)index].ThumbSticks.Left.Y == 0) || (state.IsButtonDown(Buttons.DPadDown) && previousState[(int)index].IsButtonUp(Buttons.DPadDown))) && stage < numButtons - 1)
                    {
                        Input(InputType.Down, (InputController)index);

                    }
                    if (((state.ThumbSticks.Left.Y > 0f && previousState[(int)index].ThumbSticks.Left.Y == 0) || (state.IsButtonDown(Buttons.DPadUp) && previousState[(int)index].IsButtonUp(Buttons.DPadUp))) && stage > 0)
                    {

                        Input(InputType.Up, (InputController)index);

                    }

                    if (state.IsButtonUp(Buttons.A) && previousState[(int)index].IsButtonDown(Buttons.A))
                    {
                        Input(InputType.Confirm, (InputController)index);
                    }

                    if (state.IsButtonUp(Buttons.B) && previousState[(int)index].IsButtonDown(Buttons.B))
                    {
                        Input(InputType.Back, (InputController)index);
                    }

                    if ((state.ThumbSticks.Left.X > 0.2f && previousState[(int)index].ThumbSticks.Left.X < 0.2f) || (state.IsButtonDown(Buttons.DPadRight) && previousState[(int)index].IsButtonUp(Buttons.DPadRight)))
                    {
                        Input(InputType.Left, (InputController)index);
                    }
                    if ((state.ThumbSticks.Left.X < -0.2f && previousState[(int)index].ThumbSticks.Left.X > -0.2f) || (state.IsButtonDown(Buttons.DPadLeft) && previousState[(int)index].IsButtonUp(Buttons.DPadLeft)))
                    {
                        Input(InputType.Right, (InputController)index);
                    }
                    previousState[(int)index] = state;
                }
            }
        
        }

        protected void KeyBoardInput()
        {
            KeyboardState state = Keyboard.GetState();
            if (((state.IsKeyDown(Keys.Down) && previousKeyBoardState.IsKeyUp(Keys.Down)) || (state.IsKeyDown(Keys.S) && previousKeyBoardState.IsKeyUp(Keys.S))) && stage < numButtons - 1)
            {
                Input(InputType.Down, InputController.KeyBoard);

            }
            if (((state.IsKeyDown(Keys.Up) && previousKeyBoardState.IsKeyUp(Keys.Up)) || (state.IsKeyDown(Keys.W) && previousKeyBoardState.IsKeyUp(Keys.W))) && stage > 0)
            {
                Input(InputType.Up, InputController.KeyBoard);
            }

            if (((state.IsKeyDown(Keys.Left) && previousKeyBoardState.IsKeyUp(Keys.Left)) || (state.IsKeyDown(Keys.A) && previousKeyBoardState.IsKeyUp(Keys.A))))
            {
                Input(InputType.Left, InputController.KeyBoard);
            }
            if (((state.IsKeyDown(Keys.Right) && previousKeyBoardState.IsKeyUp(Keys.Right)) || (state.IsKeyDown(Keys.D) && previousKeyBoardState.IsKeyUp(Keys.D))))
            {
                Input(InputType.Right, InputController.KeyBoard);
            }

            if (state.IsKeyUp(Keys.Enter) && previousKeyBoardState.IsKeyDown(Keys.Enter))
            {
                Input(InputType.Confirm, InputController.KeyBoard);
            }

            if (state.IsKeyUp(Keys.Back) && previousKeyBoardState.IsKeyDown(Keys.Back))
            {
                Input(InputType.Back, InputController.KeyBoard);
            }

            previousKeyBoardState = state;
        }

        
        public void RemoteInput(InputType remoteInputType, int clientID)
        {
            Input(remoteInputType, clientID, InputController.Remote);
        }

        private void Input(InputType inputType, InputController inputController)
        {
            Input(inputType, ID_Manager.Instance.ClientNumber,inputController);
        }

        private void Input(InputType inputType, int clientID, InputController inputController)
        {
            switch (inputType)
            {
                case InputType.Up:
                    Up(clientID,inputController);
                    break;
                case InputType.Down:
                    Down(clientID, inputController);
                    break;
                case InputType.Confirm:
                    ConfirmSelection(clientID, inputController);
                    break;
                case InputType.Back:
                    Back(clientID, inputController);
                    stage = 0;
                    break;
                case InputType.Left:
                    Left(clientID, inputController);
                    break;
                case InputType.Right:
                    Right(clientID, inputController);
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
