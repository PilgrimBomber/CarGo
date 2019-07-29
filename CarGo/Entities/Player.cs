using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace CarGo
{
    public enum CarType { Small, Medium, Big}
    

    public class Player : Entity
    {
        private InputHandler inputHandler;
        private float maxSpeed;
        private float acceleration;
        private float turnRate;
        private float drift;
        private PlayerIndex playerIndex;
        private float lastTurn;
        private CarFront carFront;
        private CarFrontType carFrontType;
        private ActiveAbility active;
        private bool noDamage;
        private float cooldownBoost = 0;

        //public Vector2 Velocity { get => velocity; set => velocity = value; }
        public float MaxSpeed { get => maxSpeed; set => maxSpeed = value; }
        public float Acceleration { get => acceleration; set => acceleration = value; }
        public PlayerIndex PlayerIndex { get => playerIndex; set => playerIndex = value; }

        public Player(SoundCollection soundCollection, TextureCollection textureCollection, Scene scene, PlayerIndex playerIndex, Vector2 center, CarType carType, CarFrontType frontType, AbilityType ability)
        {
            this.playerIndex = playerIndex;
            this.scene = scene;
            inputHandler = new InputHandler(this, playerIndex);
           
            switch(carType)
            {
                case CarType.Small:
                    texture = textureCollection.GetTexture(TextureType.Car_Small);
                    acceleration = 0.12f;
                    maxSpeed = 12.0f;
                    turnRate = 1.5f;//1 is default
                    drift = 0.15f;//number between 0 and 1
                    hitpoints = 500;
                    break;
                case CarType.Medium:
                    texture = textureCollection.GetTexture(TextureType.Car_Medium);
                    acceleration = 0.12f;
                    maxSpeed = 12.0f;
                    turnRate = 1.5f;//1 is default
                    drift = 0.15f;//number between 0 and 1
                    hitpoints = 1000;
                    break;
                case CarType.Big:
                    texture = textureCollection.GetTexture(TextureType.Car_Big);
                    acceleration = 0.12f;
                    maxSpeed = 12.0f;
                    turnRate = 1.5f;//1 is default
                    drift = 0.15f;//number between 0 and 1
                    hitpoints = 1500;
                    break;
            }

            switch (ability)
            {
                case AbilityType.RocketLauncher:
                    this.active = new RocketLauncher(this);
                    break;
            }

            hitbox = new RotRectangle(0, center, new Vector2(texture.Width / 2, texture.Height / 2));
            carFront = new CarFront(soundCollection, textureCollection, frontType, hitbox);
            carFrontType = frontType;
            noDamage = false;

        }

        override public void Update(GameTime gameTime)
        {
            if (hitpoints <= 0) scene.KillEntity(this);
            lastTurn = 0;
            inputHandler.HandleInput();

            CalculateCooldowns(gameTime);
            Move(velocity);

            //Slow the car over time
            velocity *= 0.98f;
            //if (velocity.Length() < 0.03) velocity *= 0;

        }


        public override void Collide(Entity entity, EntityCategory entityCategory)
        {

            Turn(-lastTurn);
            Move((hitbox.Center - entity.Hitbox.Center) * 0.0005f);

            switch (entityCategory)
            {

                //Collision with Other Players
                case EntityCategory.Player:                            
                    {
                        (entity as Player).Move(velocity);
                        velocity *= -0.1f;
                        Move(velocity);
                        break;
                    }

                //Collision with Cargo
                case EntityCategory.Cargo:
                    {
                        Move(-velocity);
                        velocity *= -0.05f;
                        break;
                    }

                //Collision with Enemies
                case EntityCategory.Enemy:
                    {
                        //Collision with Dummy
                        if (entity.GetType() == typeof(EnemyDummy))
                        {
                            if (carFront.CheckCollision(entity))
                            {
                                entity.Hitbox.Move(velocity);
                                entity.TakeDamage(CalculateDamage());
                                entity.GetPushed(velocity);
                                velocity *= 0.5f;
                                noDamage = true;
                            }
                            else
                            {
                                noDamage = false;
                                entity.Hitbox.Move(velocity);
                            }
                        }
                        break;
                    }

                //Collision with Worldobjects
                case EntityCategory.WorldObject:
                    {
                        //Collision with Rock
                        if (entity.GetType() == typeof(Rock))
                        {
                            Move(-velocity);
                            velocity *= -0.05f;
                        }

                        //Collision with Cactus
                        if (entity.GetType() == typeof(Cactus))
                        {
                            if (!(entity as Cactus).isActivated)
                            {
                                velocity *= 0.1f;
                            }
                            else
                            {
                                Turn(-lastTurn);
                            }
                        }

                        break;
                    }

                default: break;
            }
        }



        public override void GetPushed(Vector2 direction)
        {
            velocity += direction;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 offset)
        {
            spriteBatch.Draw(texture, hitbox.Center-offset, null, Color.White, hitbox.RotationRad, hitbox.Offset, 1.0f, SpriteEffects.None, 0f);
            carFront.Draw(gameTime, spriteBatch, offset);
            //Debug: Draw the corner points of the hitbox
            Texture2D point = new Texture2D(spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            point.SetData(new[] { Color.Red });
            spriteBatch.Draw(point, new Rectangle((int)Hitbox.Corners[2].X-(int)offset.X, (int)Hitbox.Corners[2].Y- (int)offset.Y, 1, 1), Color.White);
        }

        
        public void Move(Vector2 direction)
        {
            //Move the car
            hitbox.Move(direction);
            carFront.Move(direction);
        }

        //Turn the player by rad to the right
        public void Turn(float rad)
        {
            if (velocity.Length() > 0.6f)
            {
                hitbox.Rotate(rad * turnRate);
                carFront.Turn(rad * turnRate, hitbox.Center);
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
            if (velocity.Length() < maxSpeed && cooldownBoost<=0)
            {
                velocity += new Vector2(80 * acceleration * (float)Math.Sin(hitbox.RotationRad), 80 * -acceleration * (float)Math.Cos(hitbox.RotationRad));
                cooldownBoost = 3000f;
            }

        }

        public void Active()
        {
            active.Use();
        }
        public override void TakeDamage(int damage)
        {
            if (!noDamage) hitpoints -= damage;
        }
        private int CalculateDamage()
        {
            int damage;
            switch (carFrontType)
            {
                case CarFrontType.No:
                    {
                        damage = (int)velocity.Length() * 4;
                        return damage;
                    }
                case CarFrontType.Bumper:
                    {
                        damage = (int)velocity.Length() * 4;
                        return damage;
                    }
                case CarFrontType.Spikes:
                    {
                        damage = (int)velocity.Length() * 8;
                        return damage;
                    }
                default:
                    {
                        return 0;
                    }
            }
        }

        private void CalculateCooldowns(GameTime gameTime)
        {
            if (cooldownBoost > 0)
            {
                cooldownBoost -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
        }
    }
}
