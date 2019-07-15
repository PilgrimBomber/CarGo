using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CarGo
{
    public enum CarFrontType { Spikes, Bumper }
    class CarFront
    {
        private CarFrontType carFront;
        private Texture2D texture;
        private RotRectangle Hitbox;

        public CarFront(CarFrontType frontType, ContentManager content, RotRectangle CarHitbox)
        {
            carFront = frontType;
            switch (frontType)
            {
                case CarFrontType.Bumper:
                    texture = content.Load<Texture2D>("Bumper");
                    break;
                case CarFrontType.Spikes:
                    texture = content.Load<Texture2D>("Spikes");
                    break;
            }
            Hitbox = new RotRectangle(0, (CarHitbox.Corners[1] + CarHitbox.Corners[2]) / 2, new Vector2(texture.Width / 2, texture.Height / 2));
        }

        public void Turn(float rad, Vector2 RotationOrigin)
        {
            Hitbox.RotatePoint(rad, RotationOrigin);
        }

        public void Move(Vector2 direction)
        {
            Hitbox.Move(direction);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 offset)
        {
            spriteBatch.Draw(texture, Hitbox.Center - offset, null, Color.White, Hitbox.RotationRad, Hitbox.Offset, 1.0f, SpriteEffects.None, 0f);
        }

        public bool CheckCollision(Entity entity)
        {
            return CollisionCheck.CheckCollision(Hitbox, entity.Hitbox);
        }
    }
}
