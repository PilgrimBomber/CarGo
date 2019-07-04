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
        public Camera()
        {
            positon =new Vector2(0,0);
        }
        public void Draw(List<Entity> entities,GameTime gameTime)
        {
            foreach (Entity entity in entities)
            {
                entity.Draw(gameTime);
            }
        }

    }
}
