﻿using System;
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
    public class EnemyDummy : BaseEnemy
    {
        SoundEffectInstance crashSound;
        
        private List<Location> path;
        public List<Location> Path { get => path; set => path = value; }

        public EnemyDummy(SoundCollection soundCollection, TextureCollection textureCollection, Scene scene, Vector2 center)
        {
            this.scene = scene;
            //Set dummy texture
            texture = textureCollection.GetTexture(TextureType.Dummy);
            crashSound = soundCollection.getInstance(SoundType.Crash_Dummy);
            this.hitbox = new RotRectangle(0, center, new Vector2(texture.Width / 2, texture.Height / 2));
            velocity *= 0f;
            wasPushed = false;
        }
        override public void Update()
        {
            FollowPath();
            //Move the Dummy
            hitbox.Move(velocity);

            //Slows the dummy over time
            if(wasPushed)
            {
                velocity *= 0.96f;
                if (velocity.Length() < 0.05)
                {
                    velocity *= 0;
                    wasPushed = false;
                }
            }
            
        }



        public override void Collide(Entity entity)
        {
            hitbox.Move((hitbox.Center - entity.Hitbox.Center) * 0.005f);
            if (noCollision)
            {
                noCollision = false;
                return;
            }
            if (entity.GetType() == typeof(EnemyDummy) && !noCollision)
            {
                Vector2 otherVelocity = (entity as EnemyDummy).Velocity;
                (entity as EnemyDummy).Velocity *= -0.1f;
                (entity as EnemyDummy).Velocity += velocity*0.4f;
                (entity as EnemyDummy).noCollision = true;
                velocity *= -0.1f;
                velocity += otherVelocity * 0.4f;
                entity.Hitbox.Move(velocity);
                entity.Hitbox.Move((entity.Hitbox.Center- hitbox.Center) * 0.05f);
                Hitbox.Move(velocity);
            }
            
            //Collision with Cargo
            if (entity.GetType() == typeof(Cargo))
            {
                //Hitbox.Move((hitbox.Center - entity.Hitbox.Center) * 0.0005f);
                Hitbox.Move(-velocity);
                velocity *= -0.05f;

            }

        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 offset)
        {
            spriteBatch.Draw(texture, hitbox.Center-offset, null, Color.White, hitbox.RotationRad, hitbox.Offset, 1.05f, SpriteEffects.None, 0f);
        }
        public override void GetPushed(Vector2 direction)
        {
            velocity += 1.5f * direction;
            crashSound.Volume = 0.1f;
            crashSound.Play();
            wasPushed = true;
        }

        private void FollowPath()
        {
            if(path!=null)if (Tilemap.CoordinatesWorldToGrid(this).Equals(path.First()))
            {
                path.RemoveAt(0);
                velocity = (Tilemap.CoordinatesGridToWorld(path.First())-this.hitbox.Center);
                velocity.Normalize();
                velocity *= 2.5f;
            }
        }
    }
}
