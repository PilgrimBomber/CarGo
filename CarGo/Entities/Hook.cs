﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CarGo
{
    public class Hook : Entity
    {
        override public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Collide(Entity entity, EntityCategory entityCategory)
        {
            throw new NotImplementedException();
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 offset)
        {
            throw new NotImplementedException();
        }
        public override void GetPushed(Vector2 direction)
        {
            throw new NotImplementedException();
        }
        public override void TakeDamage(int damage)
        {
            throw new NotImplementedException();
        }

        public override void UpdateVolume()
        {
            throw new NotImplementedException();
        }
    }
}
