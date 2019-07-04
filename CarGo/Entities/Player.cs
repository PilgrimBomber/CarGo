﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CarGo.Entities
{
    class Player: Entity
    {
        public InputHandler inputHandler;
        
      

        public Player(ContentManager content)
        {
            inputHandler = new InputHandler();
            hitbox = new RotRectangle(20,new Rectangle(400, 400, 200, 450));
            // load Texture
            texture = content.Load<Texture2D>("Car1");
        }

        override public void Update()
        {
            throw new NotImplementedException();
        }

        public override void Collide()
        {
            throw new NotImplementedException();
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, hitbox.middle, null, Color.White, hitbox.rotationRad, hitbox.offset,1, SpriteEffects.None,0f);
        }
    }
}
