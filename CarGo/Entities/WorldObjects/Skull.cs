using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace CarGo
{
    public class Skull : WorldObject
    {
        public bool isActivated;
        public bool isExploded;
        Texture2D ripTexture;
        public Skull(Scene scene, Vector2 center,int objectID)
        {
            this.objectID = objectID;
            this.scene = scene;
            texture = TextureCollection.Instance.GetTexture(TextureType.WorldObject_Skull);
            ripTexture = TextureCollection.Instance.GetTexture(TextureType.WorldObject_SkullRip);
            this.hitbox = new RotRectangle(0, center, new Vector2(texture.Width / 2, texture.Height / 2));
            isActivated = false;
            UpdateVolume();
        }
        override public void Update(GameTime gameTime)
        {
            //throw new NotImplementedException();
        }

        public override void Collide(Entity entity, EntityCategory entityCategory)
        {
            if (!isActivated)
            {
                isActivated = true;
            }

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 offset)
        {
            if (isActivated)
            {
                spriteBatch.Draw(ripTexture, hitbox.Center - offset, null, Color.White, hitbox.RotationRad, hitbox.Offset, 1.05f, SpriteEffects.None, 0f);
            }
            else
            {
                spriteBatch.Draw(texture, hitbox.Center - offset, null, Color.White, hitbox.RotationRad, hitbox.Offset, 1.05f, SpriteEffects.None, 0f);
            }

        }
        public override void GetPushed(Vector2 direction)
        {
            throw new NotImplementedException();
        }
        public override void TakeDamage(int damage)
        {
            //throw new NotImplementedException();
        }

        public override void UpdateVolume()
        {
        }
    }
}