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

        public Cargo (Scene scene, Vector2 center)
        {
            this.scene = scene;
            texture = TextureCollection.Instance.GetTexture(TextureType.Cargo);
            this.hitbox = new RotRectangle(0, center, new Vector2(texture.Width / 2, texture.Height / 2));
            carFront = new CarFront(CarFrontType.No, CarType.Big, hitbox);
            hitbox.Rotate(Geometry.DegToRad(90));
            carFront.Hitbox.RotatePoint(Geometry.DegToRad(90), hitbox.Center);
            this.velocity = new Vector2(2f, 0);
            maxLife = 1000;
            hitpoints = 1000;
            UpdateVolume();
        }
        override public void Update(GameTime gameTime)
        {
            if (hitpoints <= 0)
            {
                scene.Finish(false);
                hitpoints = 1000;
            }
            hitbox.Move(velocity);
            carFront.Move(velocity);
            //throw new NotImplementedException();
        }

        public override void Collide(Entity entity, EntityCategory entityCategory)
        {
            if (carFront.CheckCollision(entity))
            {
                entity.Velocity += velocity;
                //entity.noCollision = true;

                switch(entityCategory)
                {
                    case EntityCategory.Player: (entity as Player).Move(velocity*1.05f); break;
                    case EntityCategory.Enemy: entity.Velocity += velocity; (entity as BaseEnemy).wasPushed = true; break;
                }
                
            }
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
            hitpoints -= damage;
        }

        public override void UpdateVolume()
        {

        }
    }
}
