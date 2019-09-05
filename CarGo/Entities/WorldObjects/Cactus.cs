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
    public class Cactus : WorldObject
    {
        public bool isActivated;
        Texture2D ripTexture;
        public Cactus(SoundCollection soundCollection, TextureCollection textureCollection, Scene scene, Vector2 center)
        {
            this.scene = scene;
            texture = textureCollection.GetTexture(TextureType.WorldObject_Cactus);
            ripTexture = textureCollection.GetTexture(TextureType.WorldObject_CactusRip);
            this.hitbox = new RotRectangle(0, center, new Vector2(texture.Width / 2, texture.Height / 2));
            isActivated = false;
        }
        override public void Update(GameTime gameTime)
        {
            //if (isActivated == true) scene.KillEntity(this);
        }

        public override void Collide(Entity entity, EntityCategory entityCategory)
        {
            if(!isActivated)
            {
                isActivated = true;
                
            }
             
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 offset)
        {
            if(isActivated)
            {
                spriteBatch.Draw(ripTexture, hitbox.Center - offset, null, Color.White, hitbox.RotationRad, hitbox.Offset, 1.05f, SpriteEffects.None, 0f);
            }
            else
            {
                spriteBatch.Draw(texture, hitbox.Center - offset, null, Color.White, hitbox.RotationRad, hitbox.Offset, 1.05f, SpriteEffects.None, 0f);
            }
            
        }
        public override void GetPushed(Vector2 direction)
        {
            throw new NotImplementedException();
        }
        public override void TakeDamage(int damage)
        {
            
        }
    }
}