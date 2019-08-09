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
    public class RocketLauncher:ActiveAbility
    {
        private SoundEffectInstance soundExplosion;
        private SoundEffectInstance soundLaunch;
        private Texture2D textureExplosion;
        private Player player;
        private bool isExploded;

        public RocketLauncher(SoundCollection soundCollection, TextureCollection textureCollection, Scene scene, Player player)
        {
            isActive = false;
            activationCooldownTimer = 0;
            livingTimer = 0;

            this.player = player;
            this.scene = scene;
            
            texture = textureCollection.GetTexture(TextureType.Active_RocketLauncher);
            textureExplosion = textureCollection.GetTexture(TextureType.Explosion);
            soundLaunch = soundCollection.GetInstance(SoundType.RocketLauncher_Launch);
            soundExplosion = soundCollection.GetInstance(SoundType.RocketLauncher_Explosion);

            hitbox = new RotRectangle(player.Hitbox.RotationRad, player.Hitbox.Center, new Vector2(texture.Width / 2, texture.Height / 2));
        }


        public void Explosion()
        {
            soundExplosion.Volume = 1;
            soundExplosion.Play();
            isExploded = true;
            livingTimer = 1;
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
                        entity1.TakeDamage(99);
                    }
                }
            }
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 offset)
        {
            if (isActive)
            {
                if (isExploded)
                {
                    spriteBatch.Draw(textureExplosion, hitbox.Center - offset, null, Color.White, 0, new Vector2(textureExplosion.Width / 2, textureExplosion.Height / 2), 1.0f, SpriteEffects.None, 0f);
                }
                else
                {
                    spriteBatch.Draw(texture, hitbox.Center - offset, null, Color.White, hitbox.RotationRad, hitbox.Offset, 1.0f, SpriteEffects.None, 0f);
                }
                
            }
            
        }


        public override void Update(GameTime gameTime)
        {
            if (isActive && !isExploded) {
                hitbox.Move(velocity);
            }
            Cooldown(gameTime);
        }

        public override void Use()
        {
            if (activationCooldownTimer > 0) return;
            velocity.X = (float)Math.Sin(player.Hitbox.RotationRad);
            velocity.Y = -(float)Math.Cos(player.Hitbox.RotationRad);
            velocity.Normalize();
            velocity *= 15;
            hitbox = new RotRectangle(player.Hitbox.RotationRad, player.Hitbox.Center, new Vector2(texture.Width / 2, texture.Height / 2));
            isExploded = false;

            soundLaunch.Play();
            isActive = true;
            activationCooldownTimer = 2f;
            livingTimer = 10f;
        }

        public override void TakeDamage(int damage)
        {
            if(!isExploded)Collide(this, EntityCategory.ActiveAbility);
        }

        public override void GetPushed(Vector2 direction)
        {
            throw new NotImplementedException();
        }
    }

}
