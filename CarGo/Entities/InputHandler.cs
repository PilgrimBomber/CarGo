using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CarGo
{
    class InputHandler
    {
        private Player player;
        private GamePadState previousState;
        private KeyboardState previousKeyBoardState;
        private InputController inputController;
        public InputHandler(Player player, InputController inputController)
        {
            this.player = player;
            if(inputController < InputController.KeyBoard)
            {
                this.inputController = inputController;
                previousState = GamePad.GetState((PlayerIndex)inputController);
            }
            if (inputController == InputController.KeyBoard) this.inputController = InputController.KeyBoard;
            previousKeyBoardState = Keyboard.GetState();
        }

        public void HandleInput()
        {
            if (StateMachine.Instance.networkGame) inputController = PreferredInput.Instance.GetPreferredInput;
            if(inputController == InputController.KeyBoard)KeyboardInput();
            if(inputController< InputController.KeyBoard) GamepadInput((PlayerIndex)inputController);
        }

        private void GamepadInput(PlayerIndex playerIndex)
        {
            //Controller Input
            GamePadCapabilities capabilities = GamePad.GetCapabilities(playerIndex);
            bool playerAction = false;
            // Check if the controller is connected
            if (capabilities.IsConnected)
            {
                GamePadState state = GamePad.GetState(playerIndex);


                if (state.ThumbSticks.Left.X < 0f)
                {
                    player.Turn(-1.0f / 180.0f * (float)Math.PI);
                    playerAction = true;
                }
                if (state.ThumbSticks.Left.X > 0f)
                {
                    player.Turn(1.0f / 180.0f * (float)Math.PI);
                    playerAction = true;
                }
                    
                if (state.Triggers.Right > 0.1)
                {
                    player.Accelerate(state.Triggers.Right);
                    playerAction = true;
                }
                   
                if (state.Triggers.Left > 0.1)
                {
                    player.Accelerate(-state.Triggers.Left * 0.5f);
                    playerAction = true;
                }
                    
                if (state.IsButtonDown(Buttons.LeftShoulder) && previousState.IsButtonUp(Buttons.LeftShoulder))
                {
                    player.Boost();
                    playerAction = true;
                }
                if (state.IsButtonDown(Buttons.Y) && previousState.IsButtonUp(Buttons.Y))
                {
                    player.Horn(1);
                }
                if (state.IsButtonDown(Buttons.X) && previousState.IsButtonUp(Buttons.X))
                {
                    player.Horn(2);
                }
                if (state.IsButtonDown(Buttons.B) && previousState.IsButtonUp(Buttons.B))
                {
                    player.Horn(3);
                }
                if (state.IsButtonDown(Buttons.A)|| state.IsButtonDown(Buttons.RightShoulder))
                {
                    player.Active();
                }
                if(state.IsButtonDown(Buttons.Back)&& previousState.IsButtonUp(Buttons.Back))
                {
                    player.ResetPosition();
                }
                if (playerAction) player.Idle(false);
                else player.Idle(true);

                if (state.IsButtonDown(Buttons.Start))
                {
                    StateMachine.Instance.ChangeState(GameState.MenuPause);
                }

                previousState = state;

            }
        }

        private void KeyboardInput()
        {
            //KeyboardInput
            KeyboardState keyboardstate = Keyboard.GetState();
            if (keyboardstate.IsKeyDown(Keys.Right) || keyboardstate.IsKeyDown(Keys.D))
            {
                player.Turn(1.0f / 180.0f * (float)Math.PI);
            }
            if (keyboardstate.IsKeyDown(Keys.Left) || keyboardstate.IsKeyDown(Keys.A))
            {
                player.Turn(-1.0f / 180.0f * (float)Math.PI);
            }
            if (keyboardstate.IsKeyDown(Keys.Up) || keyboardstate.IsKeyDown(Keys.W))
            {
                player.Accelerate(1);
            }
            if (keyboardstate.IsKeyDown(Keys.Down) || keyboardstate.IsKeyDown(Keys.S))
            {
                player.Accelerate(-0.3f);
            }
            if (keyboardstate.IsKeyDown(Keys.LeftShift) && previousKeyBoardState.IsKeyUp(Keys.LeftShift))
            {
                player.Boost();
            }
            if (keyboardstate.IsKeyDown(Keys.H) && previousKeyBoardState.IsKeyUp(Keys.H))
            {
                player.Horn(1);
            }
            if (keyboardstate.IsKeyDown(Keys.J) && previousKeyBoardState.IsKeyUp(Keys.J))
            {
                player.Horn(2);
            }
            if (keyboardstate.IsKeyDown(Keys.Space) && previousKeyBoardState.IsKeyUp(Keys.Space))
            {
                player.Active();
            }
            if (keyboardstate.IsKeyDown(Keys.Space) && previousKeyBoardState.IsKeyUp(Keys.Space))
            {
                player.Active();
            }
            if (keyboardstate.IsKeyUp(Keys.Escape) && previousKeyBoardState.IsKeyDown(Keys.Escape))
            {
                StateMachine.Instance.ChangeState(GameState.MenuPause);
            }

            if (keyboardstate.GetPressedKeys().Length == 0) player.Idle(false);
            else player.Idle(true);

            previousKeyBoardState = keyboardstate;
        }

        public static char KeyToString(Keys input, bool shift)
        {
            char key;
            switch (input)
            {
                //Alphabet keys
                case Keys.A: if (shift) { key = 'A'; } else { key = 'a'; } return key;
                case Keys.B: if (shift) { key = 'B'; } else { key = 'b'; } return key;
                case Keys.C: if (shift) { key = 'C'; } else { key = 'c'; } return key;
                case Keys.D: if (shift) { key = 'D'; } else { key = 'd'; } return key;
                case Keys.E: if (shift) { key = 'E'; } else { key = 'e'; } return key;
                case Keys.F: if (shift) { key = 'F'; } else { key = 'f'; } return key;
                case Keys.G: if (shift) { key = 'G'; } else { key = 'g'; } return key;
                case Keys.H: if (shift) { key = 'H'; } else { key = 'h'; } return key;
                case Keys.I: if (shift) { key = 'I'; } else { key = 'i'; } return key;
                case Keys.J: if (shift) { key = 'J'; } else { key = 'j'; } return key;
                case Keys.K: if (shift) { key = 'K'; } else { key = 'k'; } return key;
                case Keys.L: if (shift) { key = 'L'; } else { key = 'l'; } return key;
                case Keys.M: if (shift) { key = 'M'; } else { key = 'm'; } return key;
                case Keys.N: if (shift) { key = 'N'; } else { key = 'n'; } return key;
                case Keys.O: if (shift) { key = 'O'; } else { key = 'o'; } return key;
                case Keys.P: if (shift) { key = 'P'; } else { key = 'p'; } return key;
                case Keys.Q: if (shift) { key = 'Q'; } else { key = 'q'; } return key;
                case Keys.R: if (shift) { key = 'R'; } else { key = 'r'; } return key;
                case Keys.S: if (shift) { key = 'S'; } else { key = 's'; } return key;
                case Keys.T: if (shift) { key = 'T'; } else { key = 't'; } return key;
                case Keys.U: if (shift) { key = 'U'; } else { key = 'u'; } return key;
                case Keys.V: if (shift) { key = 'V'; } else { key = 'v'; } return key;
                case Keys.W: if (shift) { key = 'W'; } else { key = 'w'; } return key;
                case Keys.X: if (shift) { key = 'X'; } else { key = 'x'; } return key;
                case Keys.Y: if (shift) { key = 'Y'; } else { key = 'y'; } return key;
                case Keys.Z: if (shift) { key = 'Z'; } else { key = 'z'; } return key;

                //Decimal keys
                case Keys.D0: if (shift) { key = '='; } else { key = '0'; } return key;
                case Keys.D1: if (shift) { key = '!'; } else { key = '1'; } return key;
                case Keys.D2: if (shift) { key = '"'; } else { key = '2'; } return key;
                case Keys.D3: if (shift) { key = '§'; } else { key = '3'; } return key;
                case Keys.D4: if (shift) { key = '$'; } else { key = '4'; } return key;
                case Keys.D5: if (shift) { key = '%'; } else { key = '5'; } return key;
                case Keys.D6: if (shift) { key = '&'; } else { key = '6'; } return key;
                case Keys.D7: if (shift) { key = '/'; } else { key = '7'; } return key;
                case Keys.D8: if (shift) { key = '('; } else { key = '8'; } return key;
                case Keys.D9: if (shift) { key = ')'; } else { key = '9'; } return key;

                //Decimal numpad keys
                case Keys.NumPad0: key = '0'; return key;
                case Keys.NumPad1: key = '1'; return key;
                case Keys.NumPad2: key = '2'; return key;
                case Keys.NumPad3: key = '3'; return key;
                case Keys.NumPad4: key = '4'; return key;
                case Keys.NumPad5: key = '5'; return key;
                case Keys.NumPad6: key = '6'; return key;
                case Keys.NumPad7: key = '7'; return key;
                case Keys.NumPad8: key = '8'; return key;
                case Keys.NumPad9: key = '9'; return key;

                //Special keys
                case Keys.OemTilde: if (shift) { key = '~'; } else { key = '`'; } return key;
                case Keys.OemSemicolon: if (shift) { key = ':'; } else { key = ';'; } return key;
                case Keys.OemQuotes: if (shift) { key = '"'; } else { key = '\''; } return key;
                case Keys.OemQuestion: if (shift) { key = '?'; } else { key = '/'; } return key;
                case Keys.OemPlus: if (shift) { key = '+'; } else { key = '='; } return key;
                case Keys.OemPipe: if (shift) { key = '|'; } else { key = '\\'; } return key;
                case Keys.OemPeriod: if (shift) { key = '>'; } else { key = '.'; } return key;
                case Keys.OemOpenBrackets: if (shift) { key = '{'; } else { key = '['; } return key;
                case Keys.OemCloseBrackets: if (shift) { key = '}'; } else { key = ']'; } return key;
                case Keys.OemMinus: if (shift) { key = '_'; } else { key = '-'; } return key;
                case Keys.OemComma: if (shift) { key = '<'; } else { key = ','; } return key;
                case Keys.Space: key = ' '; return key;
            }
            return ' ';
        }
    }
}
