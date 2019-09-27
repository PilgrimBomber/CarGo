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

        public Shockwave()
        {
            //isActive = false;
            //activationCooldownTimer = 0;
            //livingTimer = 0;

            //this.player = player;
            //this.scene = scene;


            //texture = TextureCollection.getInstance().GetTexture(TextureType.Active_RocketLauncher);
            //textureExplosion = TextureCollection.getInstance().GetTexture(TextureType.Explosion);
            //soundLaunch = SoundCollection.getInstance().GetSoundInstance(SoundType.RocketLauncher_Launch);
            //soundLaunch.Volume = 0.225f;
            //soundExplosion = SoundCollection.getInstance().GetSoundInstance(SoundType.RocketLauncher_Explosion);
            //soundExplosion.Volume = 1f;
            //hitbox = new RotRectangle(player.Hitbox.RotationRad, player.Hitbox.Center, new Vector2(texture.Width / 2, texture.Height / 2));
            // animation = new Animation(AnimationType.Explosion, new RotRectangle(hitbox.RotationRad, hitbox.Center /* -offset */, new Vector2(textureExplosion.Width / 2, textureExplosion.Height / 2)));
        }

        public override void Collide(Entity entity, EntityCategory entityCategory)
        {
            throw new NotImplementedException();
        }

        public override void GetPushed(Vector2 direction)
        {
            throw new NotImplementedException();
        }

        public override void TakeDamage(int damage)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Use()
        {
            throw new NotImplementedException();
        }
    }
}
