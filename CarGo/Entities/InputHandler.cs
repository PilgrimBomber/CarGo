using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CarGo.Entities
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
            KeyboardInput();
            GamepadInput();           
        }

        private void GamepadInput()
        {
            //Controller Input
            GamePadCapabilities capabilities = GamePad.GetCapabilities(player.PlayerIndex);

            // Check if the controller is connected
            if (capabilities.IsConnected)
            {
                GamePadState state = GamePad.GetState(player.PlayerIndex);

                
                if (state.ThumbSticks.Left.X < 0f)
                    player.Turn(-1);
                if (state.ThumbSticks.Left.X > 0f)
                    player.Turn(1);
                if (state.Triggers.Right > 0.1)
                    player.Accelerate(state.Triggers.Right);
                if (state.Triggers.Left > 0.1)
                    player.Accelerate(-state.Triggers.Left/3);
                if(state.IsButtonDown(Buttons.LeftShoulder)&&previousState.IsButtonUp(Buttons.LeftShoulder))
                {
                    player.Boost();
                }
                
                previousState = state;

            }
        }

        private void KeyboardInput()
        {
            //KeyboardInput
            KeyboardState keyboardstate = Keyboard.GetState();
            if (keyboardstate.IsKeyDown(Keys.Right))
            {
                player.Turn(1);
            }
            if (keyboardstate.IsKeyDown(Keys.Left))
            {
                player.Turn(-1);
            }
            if (keyboardstate.IsKeyDown(Keys.Up))
            {
                player.Accelerate(1);
            }
            if (keyboardstate.IsKeyDown(Keys.Down))
            {
                player.Accelerate(-0.3f);
            }
            if (keyboardstate.IsKeyDown(Keys.LeftShift) && previousKeyBoardState.IsKeyUp(Keys.LeftShift))
            {
                player.Boost();
            }

            previousKeyBoardState = keyboardstate;
        }
    }
}
