using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace CarGo
{
    public class FlameThrower : ActiveAbility
    {
        private int damagePerTick;
        private bool damageTick;
        private TimeSpan tickTimer;
        private SoundEffectInstance soundFlame;
        private Animation animation;
        public FlameThrower(Scene scene, Player player):base(scene,player)
        {
            damage = 400;
            resetActivationCooldownTimer = 6;
            resetLivingTimer = 4;
            damagePerTick = (int)(damage / (resetLivingTimer* 4));
            
            texture = TextureCollection.getInstance().GetTexture(TextureType.Active_FlameThrower);
            hitbox = new RotRectangle(0,new Vector2((player.Hitbox.Corners[1].X+ player.Hitbox.Corners[2].X)/2, player.Hitbox.Corners[2].Y - texture.Height / 2), new Vector2(texture.Width / 2, texture.Height / 2));
            
            damageTick = false;
            tickTimer = new TimeSpan(0,0,0,0,0);
            soundFlame = SoundCollection.getInstance().GetSoundInstance(SoundType.FlameThrower);
            soundFlame.Volume = 0.4f;
            animation = new Animation(AnimationType.Flamethrower, hitbox);
        }


        public override void Collide(Entity entity, EntityCategory entityCategory)
        {
            if (damageTick)
            {
                switch(entityCategory)
                {
                    case EntityCategory.Enemy:
                        entity.TakeDamage(damagePerTick);
                        break;
                }
            }
        }

        

        public override void Update(GameTime gameTime)
        {
            Cooldown(gameTime);
            if (damageTick) damageTick = false;

            tickTimer += gameTime.ElapsedGameTime;
            if (tickTimer.TotalMilliseconds > 250)
            {
                tickTimer=tickTimer.Subtract(new TimeSpan(0, 0, 0, 0, 250));
                damageTick = true;
            }

        }

        public override void Use()
        {
            if (activationCooldownTimer > 0) return;
            base.Use();
            soundFlame.Play();
            damageTick = true;
            animation.Reset();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 offset)
        {
            if (isActive) animation.Draw(gameTime, spriteBatch, offset);//spriteBatch.Draw(texture, hitbox.Center - offset, null, Color.White, hitbox.RotationRad, hitbox.Offset, 1.0f, SpriteEffects.None, 0f);
        }


        public override void GetPushed(Vector2 direction)
        {

        }

        public override void TakeDamage(int damage)
        {
        }
    }
}
