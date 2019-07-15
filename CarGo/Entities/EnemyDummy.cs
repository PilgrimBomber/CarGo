using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CarGo
{
    public class EnemyDummy : BaseEnemy
    {
        

        public EnemyDummy(ContentManager content, Vector2 center)
        {
            //Set dummy texture
            texture = content.Load<Texture2D>("textures/Enemy_Dummy");
            this.hitbox = new RotRectangle(0, center, new Vector2(texture.Width / 2, texture.Height / 2));

            velocity *= 0f;
        }
        override public void Update()
        {
            //Move the Dummy
            hitbox.Move(velocity);

            //Slows the dummy over time
            velocity *= 0.94f;
            if (velocity.Length() < 0.05) velocity *= 0;
        }

        public override void Collide(Entity entity)
        {
            if (entity.GetType() == typeof(EnemyDummy))
            {
                entity.Hitbox.Move(velocity);
                velocity *= -0.1f;
                Hitbox.Move(velocity);
            }

            //Collision with Cargo
            if (entity.GetType() == typeof(Cargo))
            {
                Hitbox.Move((hitbox.Center - entity.Hitbox.Center) * 0.0005f);
                Hitbox.Move(-velocity);
                velocity *= -0.05f;

            }
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 offset)
        {
            spriteBatch.Draw(texture, hitbox.Center-offset, null, Color.White, hitbox.RotationRad, hitbox.Offset, 1.05f, SpriteEffects.None, 0f);
        }
        public override void GetPushed(Vector2 direction)
        {
            velocity += 1.5f * direction;
        }
    }
}
