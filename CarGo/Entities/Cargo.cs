using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CarGo
{
    public class Cargo:Entity
    {
        private CarFront carFront;
        public Cargo (SoundCollection soundCollection, TextureCollection textureCollection, Scene scene, Vector2 center)
        {
            this.scene = scene;
            texture = textureCollection.GetTexture(TextureType.Cargo);
            this.hitbox = new RotRectangle(0, center, new Vector2(texture.Width / 2, texture.Height / 2));
            carFront = new CarFront(soundCollection, textureCollection, CarFrontType.No, hitbox);
            hitbox.Rotate(Geometry.DegToRad(90));
            carFront.Hitbox.RotatePoint(Geometry.DegToRad(90), hitbox.Center);
            this.velocity = new Vector2(2f, 0);
            hitpoints = 1000;
            
        }
        override public void Update()
        {
            if (hitpoints <= 0) velocity *= 0;
            hitbox.Move(velocity);
            carFront.Move(velocity);
            //throw new NotImplementedException();
        }

        public override void Collide(Entity entity, EntityType entityType)
        {
            if (carFront.CheckCollision(entity))
            {
                entity.Velocity += velocity;
                entity.noCollision = true;

                switch(entityType)
                {
                    case EntityType.Player: (entity as Player).Move(velocity); break;
                    case EntityType.Enemy: entity.Velocity += velocity; (entity as BaseEnemy).wasPushed = true; break;
                    default: entity.TakeDamage(10000); break;
                }
                
            }
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 offset)
        {
            spriteBatch.Draw(texture, hitbox.Center - offset, null, Color.White, hitbox.RotationRad, hitbox.Offset, 1.05f, SpriteEffects.None, 0f);
        }

        public override void GetPushed(Vector2 direction)
        {
            throw new NotImplementedException();
        }
        public override void TakeDamage(int damage)
        {
            hitpoints -= damage;
        }
    }
}
