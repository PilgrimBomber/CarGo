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
        public Cargo (ContentManager content, Vector2 center)
        {
            texture = content.Load<Texture2D>("textures/Cargo");
            this.hitbox = new RotRectangle(0, center, new Vector2(texture.Width / 2, texture.Height / 2));
            carFront = new CarFront(CarFrontType.No, content, hitbox);
            hitbox.Rotate(Geometry.DegToRad(90));
            carFront.Hitbox.RotatePoint(Geometry.DegToRad(90), hitbox.Center);
            this.velocity = new Vector2(2f, 0);
            
            
        }
        override public void Update()
        {
            hitbox.Move(velocity);
            carFront.Move(velocity);
            //throw new NotImplementedException();
        }

        public override void Collide(Entity entity)
        {
            if (carFront.CheckCollision(entity))
            {
                entity.Velocity += velocity;
                //entity.Hitbox.Move(velocity);
                entity.noCollision = true;


                if (entity.GetType() == typeof(Player))
                {
                    (entity as Player).Move(velocity);
                    //(entity as Player).Velocity += velocity;//entity.Hitbox.Move(velocity);
                }
                else
                {
                    entity.GetPushed(velocity);
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
    }
}
