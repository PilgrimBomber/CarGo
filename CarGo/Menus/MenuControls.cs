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
    public class MenuControls:Menu
    {
        private Texture2D MenuControlsBackground;
        public MenuControls(SpriteBatch spriteBatchInit, Game1 game):base(spriteBatchInit,game,0)
        {
            

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



        protected override void Back(int clientID, InputController inputController)
        {
            StateMachine.Instance.ChangeState(GameState.MenuMain);
        }

        protected override void ConfirmSelection(int clientID, InputController inputController)
        {
            StateMachine.Instance.ChangeState(GameState.MenuMain);
        }

        
    }
}
