using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
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
        private CarType carType;
        private CarFront carFront;
        private CarFrontType carFrontType;
        private ActiveAbility active;
        private AbilityType abilityType;
        private bool noDamage;
        private float cooldownBoost = 0;
        private int idleCounter;
        private SoundEffectInstance soundAcceleration;
        private SoundEffectInstance soundBackground;
        private SoundEffectInstance soundBoost;
        private SoundEffectInstance soundHorn;
        private SoundEffectInstance soundHorn2;

        //public Vector2 Velocity { get => velocity; set => velocity = value; }
        public float TurnRate { get => turnRate; set => turnRate = value; }
        public float Drift { get => drift; set => drift = value; }
        public float MaxSpeed { get => maxSpeed; set => maxSpeed = value; }
        public float Acceleration { get => acceleration; set => acceleration = value; }
        public PlayerIndex PlayerIndex { get => playerIndex; set => playerIndex = value; }

        public Player(Scene scene, PlayerIndex playerIndex, Vector2 center, CarType carType, CarFrontType frontType, AbilityType ability)
        {
            this.playerIndex = playerIndex;
            this.scene = scene;
            inputHandler = new InputHandler(this, playerIndex);
            soundAcceleration = SoundCollection.getInstance().GetSoundInstance(SoundType.Car_Accelerate);
            soundBackground = SoundCollection.getInstance().GetSoundInstance(SoundType.Car_Background);
            soundBackground.Volume = 0.07f;
            soundBoost = SoundCollection.getInstance().GetSoundInstance(SoundType.Car_Boost);
            soundBoost.Volume = 0.1f;
            soundHorn = SoundCollection.getInstance().GetSoundInstance(SoundType.Car_Horn);
            soundHorn.Volume = 0.3f;
            soundHorn2 = SoundCollection.getInstance().GetSoundInstance(SoundType.Car_Horn2);
            soundHorn2.Volume = 0.2f;
            this.carType = carType;
            switch (carType)
            {
                case CarType.Small:
                    texture = TextureCollection.getInstance().GetTexture(TextureType.Car_Small);
                    acceleration = 0.1f;
                    maxSpeed = 12.0f;
                    turnRate = 2.5f;//1 is default
                    drift = 0.05f;//number between 0 and 1
                    hitpoints = 500;
                    break;
                case CarType.Medium:
                    texture = TextureCollection.getInstance().GetTexture(TextureType.Car_Medium);
                    acceleration = 0.08f;
                    maxSpeed = 14.0f;
                    turnRate = 1.8f;//1 is default
                    drift = 0.15f;//number between 0 and 1
                    hitpoints = 1000;
                    break;
                case CarType.Big:
                    texture = TextureCollection.getInstance().GetTexture(TextureType.Car_Big);
                    acceleration = 0.05f;
                    maxSpeed = 16.0f;
                    turnRate = 1.3f;//1 is default
                    drift = 0.05f;//number between 0 and 1
                    hitpoints = 1500;
                    break;
            }
            hitbox = new RotRectangle(0, center, new Vector2(texture.Width / 2, texture.Height / 2));
            this.abilityType = ability;
            switch (ability)
            {
                case AbilityType.RocketLauncher:
                    this.active = new RocketLauncher(scene, this);
                    break;
                case AbilityType.Shockwave:
                    this.active = new Shockwave(scene, this);
                    break;
                case AbilityType.TrapLauncher:
                    this.active = new TrapLauncher(scene, this);
                    break;
                case AbilityType.Flamethrower:
                    this.active = new FlameThrower(scene, this);
                    break;
            }
            scene.addActiveAbility(active);

            
            carFront = new CarFront(frontType, hitbox);
            carFrontType = frontType;
            noDamage = false;
            idleCounter = 0;
        }

        override public void Update(GameTime gameTime)
        {
            if (hitpoints <= 0) scene.KillEntity(this);
            lastTurn = 0;
            inputHandler.HandleInput();

            CalculateCooldowns(gameTime);
            //Console.WriteLine("Geschwindigkeit:" + velocity.Length().ToString() + "Position: " + hitbox.Center.ToString());
            Move(velocity);

            if(velocity.Length()>0.2)
            {
                soundBackground.Play();

            }
            else
            {
                soundBackground.Pause();
                
            }

            //Slow the car over time
            if (idleCounter > 20) velocity *= 0.98f;
            else velocity *= 0.99f;
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
                        Vector2 velocity2 = entity.Velocity;
                        (entity as Player).Move(-velocity2);
                        Move(-velocity);
                        entity.Velocity = velocity;
                        this.velocity = velocity2;
                        //(entity as Player).Move(velocity);
                        //velocity *= -0.6f;
                        //Move(velocity);
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
                        if (entity.GetType() == typeof(EnemySlow))
                        {
                            if (carFront.CheckCollision(entity))
                            {
                                entity.Hitbox.Move(velocity);
                                entity.TakeDamage((int)CalculateDamage());
                                //entity.GetPushed(velocity);
                                switch(carType)
                                {
                                    case CarType.Big:
                                        entity.GetPushed(velocity*1.3f);
                                        velocity *= 0.8f;
                                        break;
                                    case CarType.Medium:
                                        entity.GetPushed(velocity);
                                        velocity *= 0.5f;
                                        break;
                                    case CarType.Small:
                                        entity.GetPushed(velocity*0.8f);
                                        velocity *= 0.3f;
                                        break;
                                }
                                
                                noDamage = true;
                            }
                            else
                            {
                                noDamage = false;
                                entity.Hitbox.Move(velocity);
                                entity.GetPushed(velocity * 0.1f);
                            }
                        }
                        //Collision with 
                        if (entity.GetType() == typeof(EnemyFast))
                        {
                            if (carFront.CheckCollision(entity))
                            {
                                entity.Hitbox.Move(velocity);
                                entity.TakeDamage((int)CalculateDamage());
                                //entity.GetPushed(velocity);
                                switch (carType)
                                {
                                    case CarType.Big:
                                        entity.GetPushed(velocity * 1.3f);
                                        velocity *= 0.8f;
                                        break;
                                    case CarType.Medium:
                                        entity.GetPushed(velocity);
                                        velocity *= 0.5f;
                                        break;
                                    case CarType.Small:
                                        entity.GetPushed(velocity * 0.8f);
                                        velocity *= 0.3f;
                                        break;
                                }

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
            if (abilityType == AbilityType.Flamethrower) active.Hitbox.Move(direction);
        }

        //Turn the player by rad to the right
        public void Turn(float rad)
        {
            if (velocity.Length() > 1.5f)
            {
                
                hitbox.Rotate(rad * turnRate * (30 - velocity.Length()) / 20);
                carFront.Turn(rad * turnRate * (30 - velocity.Length()) / 20, hitbox.Center);
                velocity = Geometry.Rotate(velocity, rad * turnRate * (30 - velocity.Length()) / 20 * (1 - drift));
                lastTurn = rad;

                if(abilityType== AbilityType.Flamethrower) active.Hitbox.RotatePoint(rad * turnRate * (30 - velocity.Length()) / 20, hitbox.Center);

            }

        }
        public void Accelerate(float accelerationFactor)
        {
            //Console.WriteLine("Geschwindigkeit " + velocity.Length().ToString());
            
            if (accelerationFactor>0 && velocity.Length()<0.8)
            {
                soundAcceleration.Volume = accelerationFactor/8;
                soundAcceleration.Play();
            }
            if (velocity.Length() == 0)
            {
                velocity.X = acceleration * (float)Math.Sin(hitbox.RotationRad);
                velocity.Y = -acceleration * (float)Math.Cos(hitbox.RotationRad);
            }
            else
            {

                if (velocity.Length() < maxSpeed)
                {
                    velocity.X += (float)Math.Sqrt(Math.Abs(maxSpeed - velocity.Length())) * acceleration * accelerationFactor * (float)Math.Sin(hitbox.RotationRad);
                    velocity.Y -= (float)Math.Sqrt(Math.Abs(maxSpeed - velocity.Length())) * acceleration * accelerationFactor * (float)Math.Cos(hitbox.RotationRad);
                }
            }

        }

        public void Boost()
        {
            soundBoost.Play();
            if (velocity.Length() < maxSpeed && cooldownBoost<=0)
            {
                velocity += new Vector2(80 * acceleration * (float)Math.Sin(hitbox.RotationRad), 80 * -acceleration * (float)Math.Cos(hitbox.RotationRad));
                cooldownBoost = 1000f;
            }

        }

        public void Horn(int horn_Number)
        {
            switch(horn_Number)
            {
                case 1: soundHorn.Play(); break;
                case 2: soundHorn2.Play(); break;
            }
            
        }

        public void Idle(bool performedAction)
        {
            if (performedAction) idleCounter = 0;
            else idleCounter++;
        }

        public void Active()
        {
            active.Use();
        }
        public override void TakeDamage(int damage)
        {
            if (!noDamage) hitpoints -= damage;
        }
        private float CalculateDamage()
        {
            float damage;
            
            switch (carFrontType)
            {
                case CarFrontType.No:
                    {
                        damage = (int)velocity.Length() * 4;
                        break;
                        //return damage;
                    }
                case CarFrontType.Bumper:
                    {
                        damage = (int)velocity.Length() * 4;
                        break;
                        //return damage;
                    }
                case CarFrontType.Spikes:
                    {
                        damage = (int)velocity.Length() * 8;
                        break;
                        //return damage;
                    }
                default:
                    {
                        damage = 0;break;
                        //return 0;
                    }
            }

            switch (carType)
            {
                case CarType.Big:
                    damage *= 2f;
                    break;
                case CarType.Medium:
                    damage *= 1f;
                    break;
                case CarType.Small:
                    damage *= 0.75f;
                    break;
            }

            return damage;
        }

        public void SetPosition(Vector2 position)
        {
            Move(position - hitbox.Center);
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
