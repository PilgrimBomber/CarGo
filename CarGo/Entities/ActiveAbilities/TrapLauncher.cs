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
        private SoundEffectInstance soundExplosion;
        private SoundEffectInstance soundTrapLaunch;


        public TrapLauncher(Scene scene, Player player):base(scene,player)
        {
            damage = 99;
            resetLivingTimer = 8;
            resetActivationCooldownTimer = 5;
            
            texture = TextureCollection.getInstance().GetTexture(TextureType.Active_Trap);
            hitbox = new RotRectangle(0, player.Hitbox.Center, new Vector2(texture.Width / 2, texture.Height / 2));
            isExploded = false;
            
            explosionAnimation = new Animation(AnimationType.Explosion, hitbox);
            soundExplosion = SoundCollection.getInstance().GetSoundInstance(SoundType.RocketLauncher_Explosion);
            soundTrapLaunch = SoundCollection.getInstance().GetSoundInstance(SoundType.Trap_Launch);
            soundTrapLaunch.Volume = 0.08f;
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
            soundTrapLaunch.Play();
            
            if(isExploded)
            {
                isExploded = false;
                //hitbox.Scale(1/5f);
            }
            
            explosionAnimation.Reset();
            base.Use();
        }
        public override void GetPushed(Vector2 direction)
        {
            
        }

        public override void TakeDamage(int damage)
        {
            
        }

        

        
    }
}
