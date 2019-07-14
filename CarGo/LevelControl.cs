using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace CarGo
{
    class LevelControl
    {
        private Scene scene;
        private TimeSpan timer;
        private Level level;
        public LevelControl(Scene scene, ContentManager content)
        {
            this.scene = scene;
            this.level = new Level1(scene, content);
            timer = TimeSpan.Zero;
        }

        public void Update(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime;
            level.Update(timer);
            
            
        }
    }

}
