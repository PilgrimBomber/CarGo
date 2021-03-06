﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CarGo
{
    public enum CarFrontType { Spikes,  Bumper, No }
    class CarFront
    {
        
        private CarFrontType carFront;
        private Texture2D texture;
        private RotRectangle hitbox;

        public RotRectangle Hitbox { get => hitbox; set => hitbox = value; }

        public CarFront(CarFrontType frontType, CarType carType, RotRectangle CarHitbox)
        {
            
            carFront = frontType;
            switch(frontType)
            {
                case CarFrontType.Bumper:
                    switch (carType)
                    {
                        case CarType.Small:
                            texture = TextureCollection.Instance.GetTexture(TextureType.Front_Small_Bumper);
                            Hitbox = new RotRectangle(0, (CarHitbox.Corners[1] + CarHitbox.Corners[2]) / 2, new Vector2(texture.Width / 2, texture.Height / 2));
                            break;
                        case CarType.Medium:
                            texture = TextureCollection.Instance.GetTexture(TextureType.Front_Bumper);
                            Hitbox = new RotRectangle(0, (CarHitbox.Corners[1] + CarHitbox.Corners[2]) / 2, new Vector2(texture.Width / 2, texture.Height / 2));
                            break;
                        case CarType.Big:
                            texture = TextureCollection.Instance.GetTexture(TextureType.Front_Big_Bumper);
                            Hitbox = new RotRectangle(0, (CarHitbox.Corners[1] + CarHitbox.Corners[2]) / 2, new Vector2(texture.Width / 2, texture.Height / 2));
                            break;
                    }
                break;
                case CarFrontType.Spikes:
                    switch(carType)
                    {
                        case CarType.Small:
                            texture = TextureCollection.Instance.GetTexture(TextureType.Front_Small_Spikes);
                            Hitbox = new RotRectangle(0, (CarHitbox.Corners[1] + CarHitbox.Corners[2]) / 2, new Vector2(texture.Width / 2, texture.Height / 2));
                            break;
                        case CarType.Medium:
                            texture = TextureCollection.Instance.GetTexture(TextureType.Front_Spikes);
                            Hitbox = new RotRectangle(0, (CarHitbox.Corners[1] + CarHitbox.Corners[2]) / 2, new Vector2(texture.Width / 2, texture.Height / 2));
                            break;
                        case CarType.Big:
                            texture = TextureCollection.Instance.GetTexture(TextureType.Front_Big_Spikes);
                            Hitbox = new RotRectangle(0, (CarHitbox.Corners[1] + CarHitbox.Corners[2]) / 2, new Vector2(texture.Width / 2, texture.Height / 2));
                            break;
                    }
                    break;
                case CarFrontType.No:
                    Hitbox = new RotRectangle(0, (CarHitbox.Corners[1] + CarHitbox.Corners[2]) / 2, new Vector2((CarHitbox.Corners[1].X-CarHitbox.Corners[2].X)/2,30));
                    break;
            }
            
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
            if (carFront != CarFrontType.No)
            {
                spriteBatch.Draw(texture, Hitbox.Center - offset, null, Color.White, Hitbox.RotationRad, Hitbox.Offset, 1.0f, SpriteEffects.None, 0f);
            }
        }

        public bool CheckCollision(Entity entity)
        {
            return CollisionCheck.CheckCollision(Hitbox, entity.Hitbox);
        }
    }
}
