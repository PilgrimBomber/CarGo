using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CarGo
{
    class Camera
    {
        private Vector2 positon;
        private SpriteBatch spriteBatch;
        public Camera(SpriteBatch spriteBatchInit)
        {
            positon =new Vector2(0,0);
            spriteBatch = spriteBatchInit;
        }
        public void Draw(List<Entity> entities,GameTime gameTime)
        {
            spriteBatch.Begin();
            foreach (Entity entity in entities)
            {
                entity.Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();
        }

    }
}
