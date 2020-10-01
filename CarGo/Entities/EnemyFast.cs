using System;
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
    public class EnemyFast : BaseEnemy
    {
        private SoundEffectInstance soundCrash;
        
        //private List<Location> path2;
        //public List<Location> Path2 { get => path2; set => path2 = value; }

        public EnemyFast(Scene scene, Vector2 center,int objectID) : base(scene)
        {
            this.objectID = objectID;
            //Set EnemyFast texture
            texture = TextureCollection.Instance.GetTexture(TextureType.Enemy_Zombie_Fast);
            //textureCollection.GetTexture(TextureType.Enemy_Zombie);
            soundCrash = SoundCollection.Instance.GetSoundInstance(SoundType.Enemy_Hit);
            UpdateVolume();
            this.hitbox = new RotRectangle(0, center, new Vector2(texture.Width / 2, texture.Height / 2));
            animation = new Animation(AnimationType.Zombie_Fast, hitbox);
            hitpoints = 50;
            maxLife = 50;
            baseSpeed = 4;
            Damage = 50;
        }


        public override void Collide(Entity entity, EntityCategory entityCategory)
        {
            hitbox.Move((hitbox.Center - entity.Hitbox.Center) * 0.005f);
            if (noCollision)
            {
                noCollision = false;
                return;
            }

            switch (entityCategory)
            {
                //Collision with Player
                case EntityCategory.Player:
                    {
                        //entity.TakeDamage(100);
                        break;
                    }

                //Collision with Cargo
                case EntityCategory.Cargo:
                    {

                        //Hitbox.Move(-velocity);
                        entity.TakeDamage((int)(Damage * getPercentLife() / 100));
                        hitpoints = 0;
                        //velocity *= -0.05f;
                        break;
                    }

                //Collision with Enemies
                case EntityCategory.Enemy:
                    {

                        //Collision with Dummy
                        if (entity.GetType() == typeof(EnemySlow) && !noCollision)
                        {
                            if (wasPushed)
                            {
                                TakeDamage((int)(velocity - entity.Velocity).Length());
                                entity.Velocity = velocity;
                                //velocity *= 0.9f;

                            }
                            else
                            {
                                if ((entity as EnemySlow).wasPushed)
                                {
                                    TakeDamage((int)(velocity - entity.Velocity).Length());
                                    velocity = entity.Velocity;

                                    //entity.Velocity *= 0.9f;
                                }
                                else
                                {
                                    Vector2 otherVelocity = (entity as EnemySlow).Velocity;
                                    (entity as EnemySlow).Velocity *= -0.4f;
                                    (entity as EnemySlow).Velocity += velocity * 0.4f;
                                    (entity as EnemySlow).noCollision = true;
                                    velocity *= -0.4f;
                                    velocity += otherVelocity * 0.4f;
                                    entity.Hitbox.Move(velocity);
                                    entity.Hitbox.Move((entity.Hitbox.Center - hitbox.Center) * 0.05f);
                                    Hitbox.Move(velocity);
                                }
                            }
                        }

                        //Collision with EnemyFast
                        if (entity.GetType() == typeof(EnemyFast) && !noCollision)
                        {
                            if (wasPushed)
                            {
                                TakeDamage((int)(velocity - entity.Velocity).Length());
                                entity.Velocity = velocity;
                                //velocity *= 0.9f;

                            }
                            else
                            {
                                if ((entity as EnemyFast).wasPushed)
                                {
                                    TakeDamage((int)(velocity - entity.Velocity).Length());
                                    velocity = entity.Velocity;

                                    //entity.Velocity *= 0.9f;
                                }
                                else
                                {
                                    Vector2 otherVelocity = (entity as EnemyFast).Velocity;
                                    (entity as EnemyFast).Velocity *= -0.4f;
                                    (entity as EnemyFast).Velocity += velocity * 0.4f;
                                    (entity as EnemyFast).noCollision = true;
                                    velocity *= -0.4f;
                                    velocity += otherVelocity * 0.4f;
                                    entity.Hitbox.Move(velocity);
                                    entity.Hitbox.Move((entity.Hitbox.Center - hitbox.Center) * 0.05f);
                                    Hitbox.Move(velocity);
                                }
                            }
                        }
                        break;
                    }

                //Collision with WorldObjects
                case EntityCategory.WorldObject:
                    {
                        Vector2 direction = Hitbox.Center - entity.Hitbox.Center;

                        Hitbox.Move(direction / 15);
                        //Collision with Rock
                        if (entity.GetType() == typeof(Rock) && wasPushed == true)
                        {
                            Hitbox.Move(-velocity);
                            this.TakeDamage((int)velocity.LengthSquared());
                            velocity *= -0.05f;
                        }

                        //Collision with Cactus
                        if (entity.GetType() == typeof(Cactus) && wasPushed == true)
                        {
                            Hitbox.Move(-velocity);
                            this.TakeDamage(20);
                            velocity *= -0.05f;
                        }
                        break;
                    }
            }
        }

        public override void GetPushed(Vector2 direction)
        {
            velocity += 1.5f * direction;
            soundCrash.Volume = 0.1f;
            soundCrash.Play();
            wasPushed = true;
        }

        public override void TakeDamage(int damage)
        {
            hitpoints -= damage;
        }

        public override void UpdateVolume()
        {
            soundCrash.Volume = 1 * Settings.Instance.VolumeSound;
        }
    }
}