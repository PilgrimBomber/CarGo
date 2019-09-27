using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace CarGo
{
    public class TrapLauncher : ActiveAbility
    {

        public bool isExploded;
        private Animation explosionAnimation;
        private List<Entity> collidedEntities;
        private SoundEffectInstance soundExplosion;


        public TrapLauncher(Scene scene, Player player):base(scene,player)
        {
            texture = TextureCollection.getInstance().GetTexture(TextureType.Active_Trap);
            hitbox = new RotRectangle(0, player.Hitbox.Center, new Vector2(texture.Width / 2, texture.Height / 2));
            isExploded = false;
            damage = 99; 
            explosionAnimation = new Animation(AnimationType.Explosion, hitbox);
            soundExplosion = SoundCollection.getInstance().GetSoundInstance(SoundType.RocketLauncher_Explosion);
        }
        public override void Collide(Entity entity, EntityCategory entityCategory)
        {
            if (isExploded) return;
            Explosion();
            foreach (Entity entity1 in scene.entities)
            {
                if (entity1 == this) continue;
                if (CollisionCheck.CheckCollision(new RotRectangle(hitbox.RotationRad, hitbox.Center, new Vector2(120, 120)), entity1.Hitbox))
                {
                    if (entity1.GetType() == typeof(Player) || entity1.GetType() == typeof(Cargo))
                    {
                        entity1.TakeDamage(1);
                    }
                    else
                    {
                        entity1.TakeDamage(damage);
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 offset)
        {
            if(isActive)
            {
                if (!isExploded) spriteBatch.Draw(texture, hitbox.Center - offset, null, Color.White, hitbox.RotationRad, hitbox.Offset, 1.0f, SpriteEffects.None, 0f);
                else explosionAnimation.Draw(gameTime, spriteBatch, offset);
            }
            
        }

        public override void Update(GameTime gameTime)
        {
            Cooldown(gameTime);
        }

        public void Explosion()
        {
            soundExplosion.Play();
            isExploded = true;
            //hitbox.Scale(5);
            livingTimer = 1;
            activationCooldownTimer = 1;
        }
        public override void Use()
        {
            if (activationCooldownTimer > 0) return;
            hitbox.SetPosition(player.Hitbox.Center);

            isActive = true;
            if(isExploded)
            {
                isExploded = false;
                //hitbox.Scale(1/5f);
            }
            
            explosionAnimation.Reset();
            activationCooldownTimer = 5f;
            livingTimer = 5f;
        }
        public override void GetPushed(Vector2 direction)
        {
            
        }

        public override void TakeDamage(int damage)
        {
            
        }

        

        
    }
}
