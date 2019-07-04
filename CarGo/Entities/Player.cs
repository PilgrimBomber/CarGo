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
    class Player: Entity
    {
        public InputHandler inputHandler;
        Texture2D texture;

        public Player()
        {
            inputHandler = new InputHandler();
            
        }
        override public void Update()
        {
            throw new NotImplementedException();
        }

        public override void Collide()
        {
            throw new NotImplementedException();
        }
        public override void Draw(GameTime gameTime)
        {
            
        }
    }
}
