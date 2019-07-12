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
    class LevelControl
    {
        private Scene scene;
        private long timeCounter;
        private double timer;
        public LevelControl(Scene scene)
        {
            this.scene = scene;
            timer = 0.0;
            timeCounter = 0;
        }

        public void Update(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.TotalSeconds;
            if(timer >= 1.0)
            {
                timer -= 1.0;
                timeCounter++;
            }
        }
    }
}
