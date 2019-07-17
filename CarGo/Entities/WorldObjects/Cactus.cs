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
    public class Cactus : WorldObject
    {
        private bool isActivated;
        public Cactus(ContentManager content,Scene scene, Vector2 center)
        {
            texture = content.Load<Texture2D>("textures/Cactus");
            this.hitbox = new RotRectangle(0, center, new Vector2(texture.Width / 2, texture.Height / 2));
            isActivated = false;
            this.scene = scene;
        }
        override public void Update()
        {
            if (isActivated == true) scene.KillEntity(this);
        }

        public override void Collide(Entity entity)
        {
            isActivated = true;
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 offset)
        {
            spriteBatch.Draw(texture, hitbox.Center - offset, null, Color.White, hitbox.RotationRad, hitbox.Offset, 1.05f, SpriteEffects.None, 0f);
        }
        public override void GetPushed(Vector2 direction)
        {
            throw new NotImplementedException();
        }
    }
}