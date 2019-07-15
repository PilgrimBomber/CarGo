﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace CarGo
{
    public class Player : Entity
    {
        private InputHandler inputHandler;
        private Vector2 velocity;
        private float maxSpeed;
        private float acceleration;
        private float turnRate;
        private float drift;
        private PlayerIndex playerIndex;
        private float lastTurn;

        public Vector2 Velocity { get => velocity; set => velocity = value; }
        public float MaxSpeed { get => maxSpeed; set => maxSpeed = value; }
        public float Acceleration { get => acceleration; set => acceleration = value; }
        public PlayerIndex PlayerIndex { get => playerIndex; set => playerIndex = value; }

        public Player(ContentManager content, PlayerIndex playerIndex, Vector2 center)
        {
            this.playerIndex = playerIndex;
            inputHandler = new InputHandler(this, playerIndex);
            // load Texture
            texture = content.Load<Texture2D>("Auto-klein");
            hitbox = new RotRectangle(0, center, new Vector2(texture.Width / 2, texture.Height / 2));

            acceleration = 0.12f;
            maxSpeed = 12.0f;
            turnRate = 1.5f;//1 is default
            drift = 0.15f;//number between 0 and 1
        }
        override public void Update()
        {
            lastTurn = 0;
            inputHandler.HandleInput();

            //Move the car
            hitbox.Move(velocity);

            //Slow the car over time
            velocity *= 0.98f;
            if (velocity.Length() < 0.05) velocity *= 0;

            

        }

        public override void Collide(Entity entity)
        {
            if (entity.GetType() == typeof(Player))
            {
                entity.Hitbox.Move(velocity);
                velocity *= -0.1f;
                Hitbox.Move(velocity);

            }
            // Collision with Cargo
            if (entity.GetType() == typeof(Cargo))
            {
                Turn(-lastTurn);
                Hitbox.Move((hitbox.Center - entity.Hitbox.Center)*0.0005f);
                Hitbox.Move(-velocity);
                velocity *= -0.05f;
                
            }
            //Collision with Dummy
            if (entity.GetType() == typeof(EnemyDummy))
            {
                entity.Hitbox.Move(velocity);
                entity.GetPushed(velocity);
                velocity *= 0.5f;
            }
        }

        public override void GetPushed(Vector2 direction)
        {
            velocity += direction;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 offset)
        {
            spriteBatch.Draw(texture, hitbox.Center-offset, null, Color.White, hitbox.RotationRad, hitbox.Offset, 1.05f, SpriteEffects.None, 0f);

            //Debug: Draw the corner points of the hitbox
            Texture2D point = new Texture2D(spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            point.SetData(new[] { Color.Red });
            spriteBatch.Draw(point, new Rectangle((int)Hitbox.Corners[0].X, (int)Hitbox.Corners[0].Y, 1, 1), Color.White);
            spriteBatch.Draw(point, new Rectangle((int)Hitbox.Corners[1].X, (int)Hitbox.Corners[1].Y, 1, 1), Color.White);
            spriteBatch.Draw(point, new Rectangle((int)Hitbox.Corners[2].X, (int)Hitbox.Corners[2].Y, 1, 1), Color.White);
            spriteBatch.Draw(point, new Rectangle((int)Hitbox.Corners[3].X, (int)Hitbox.Corners[3].Y, 1, 1), Color.White);
        }

        //Turn the player by rad to the right
        public void Turn(float rad)
        {
            if (velocity.Length() > 0.6f)
            {
                hitbox.Rotate(rad * turnRate);
                velocity = Geometry.Rotate(velocity, rad * turnRate * (1 - drift));
                lastTurn = rad;
            }

        }
        public void Accelerate(float accelerationFactor)
        {
            if (velocity.Length() == 0)
            {
                velocity.X = acceleration * (float)Math.Sin(hitbox.RotationRad);
                velocity.Y = -acceleration * (float)Math.Cos(hitbox.RotationRad);
            }
            else
            {

                if (velocity.Length() < maxSpeed)
                {
                    velocity.X += (maxSpeed - velocity.Length()) / 3 * acceleration * accelerationFactor * (float)Math.Sin(hitbox.RotationRad);
                    velocity.Y -= (maxSpeed - velocity.Length()) / 3 * acceleration * accelerationFactor * (float)Math.Cos(hitbox.RotationRad);
                }
            }
        }

        public void Boost()
        {
            if (velocity.Length() < maxSpeed) velocity += new Vector2(80*acceleration * (float)Math.Sin(hitbox.RotationRad), 80*-acceleration * (float)Math.Cos(hitbox.RotationRad));

        }

    }
}
