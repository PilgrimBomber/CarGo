﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace CarGo
{
    public class Rock : WorldObject
    {
        public Rock(Scene scene, Vector2 center,int objectID)
        {
            entityType = EntityType.Rock;
            this.objectID = objectID;
            objectID = ID_Manager.Instance.GetID();
            this.scene = scene;
            texture = TextureCollection.Instance.GetTexture(TextureType.WorldObject_Rock);
            this.hitbox = new RotRectangle(0, center, new Vector2(texture.Width / 2, texture.Height / 2));
            UpdateVolume();
        }
        override public void Update(GameTime gameTime)
        {
            //throw new NotImplementedException();
        }

        public override void Collide(Entity entity, EntityCategory entityCategory)
        {
            //throw new NotImplementedException();
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 offset)
        {
            spriteBatch.Draw(texture, hitbox.Center - offset, null, Color.White, hitbox.RotationRad, hitbox.Offset, 1.05f, SpriteEffects.None, 0f);
        }
        public override void GetPushed(Vector2 direction)
        {
            
        }
        public override void TakeDamage(int damage)
        {
            
        }

        public override void UpdateVolume()
        {
        }
    }
}