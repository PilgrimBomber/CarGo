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
        public InputHandler(Player player, PlayerIndex playerIndex)
        {
            this.player = player;
            previousState = GamePad.GetState(player.PlayerIndex);
            previousKeyBoardState = Keyboard.GetState();
        }

        public void HandleInput()
        {
            //if(player.PlayerIndex==PlayerIndex.Four)KeyboardInput();

            GamepadInput();
        }

        private void GamepadInput()
        {
            //Controller Input
            GamePadCapabilities capabilities = GamePad.GetCapabilities(player.PlayerIndex);
            bool playerAction = false;
            // Check if the controller is connected
            if (capabilities.IsConnected)
            {
                GamePadState state = GamePad.GetState(player.PlayerIndex);


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

                if (player.PlayerIndex == PlayerIndex.One && state.IsButtonDown(Buttons.Start))
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
            

            if (keyboardstate.GetPressedKeys().Length == 0) player.Idle(false);
            else player.Idle(true);

            previousKeyBoardState = keyboardstate;
        }
    }
}
