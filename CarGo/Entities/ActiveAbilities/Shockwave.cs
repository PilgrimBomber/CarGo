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
    public class Shockwave:ActiveAbility
    {
        private SoundEffectInstance soundShockWave;
        private Texture2D textureShockwave;
        private Animation animation;
        private List<Entity> collidedEntities;
        public Shockwave(Scene scene, Player player):base(scene,player)
        {
            damage = 70;
            resetActivationCooldownTimer = 2.5f;
            resetLivingTimer = 0.5f;
            collidedEntities = new List<Entity>();

            textureShockwave = TextureCollection.getInstance().GetTexture(TextureType.Active_Shockwave);
            

            soundShockWave = SoundCollection.getInstance().GetSoundInstance(SoundType.Shockwave);
            soundShockWave.Volume = 0.2f;
            hitbox = new RotRectangle(player.Hitbox.RotationRad, player.Hitbox.Center, new Vector2(textureShockwave.Width / 2, textureShockwave.Height / 2));
            animation = new Animation(AnimationType.Shockwave, hitbox);
            // animation = new Animation(AnimationType.Explosion, new RotRectangle(hitbox.RotationRad, hitbox.Center /* -offset */, new Vector2(textureExplosion.Width / 2, textureExplosion.Height / 2)));
        }

        public override void Update(GameTime gameTime)
        {
            hitbox.SetPosition(player.Hitbox.Center);
            Cooldown(gameTime);
        }

        public override void Use()
        {
            if (activationCooldownTimer > 0) return;
            base.Use();
            animation.Reset();
            soundShockWave.Play();
            
        }

        public override void Collide(Entity entity, EntityCategory entityCategory)
        {
            if (collidedEntities.Contains(entity)) return;
            switch (entityCategory)
            {
                case EntityCategory.Enemy:
                    (entity as BaseEnemy).TakeDamage(damage);
                    collidedEntities.Add(entity);
                    break;
            }
        }

        public override void GetPushed(Vector2 direction)
        {
            
        }

        public override void TakeDamage(int damage)
        {
           
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 offset)
        {
            if (isActive) animation.Draw(gameTime, spriteBatch, offset);//spriteBatch.Draw(textureShockwave, hitbox.Center - offset, null, Color.White, hitbox.RotationRad, hitbox.Offset, 1.0f, SpriteEffects.None, 0f);
        }


    }
}
