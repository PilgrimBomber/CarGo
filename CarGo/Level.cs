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

    abstract class Level
    {
        protected int levelNumber;
        protected Scene scene;
        protected ContentManager content;

        public virtual void Update(TimeSpan timer)
        {
            throw new NotImplementedException();
        }
    }
    class Level1:Level
    {
        private bool[] eventTriggered;
        private int[] eventTime;
        public Level1(Scene scene, ContentManager content)
        {
            this.scene = scene;
            this.content = content;
            levelNumber = 1;
            eventTriggered = new bool[10];
            eventTime = new int[10];
            eventTime[0] = 0;
        }

        public override void Update(TimeSpan timer)
        {
            for (int i = 0; i < 10; i++)
            {
                if (timer.TotalSeconds >= eventTime[i] && !eventTriggered[i])
                {
                    switch(i)
                    {
                        case 0://Add Dummy
                            eventTriggered[0] = true;
                            scene.addEnemy(new EnemyDummy(content, new Vector2(300,200)));
                            scene.addEnemy(new EnemyDummy(content, new Vector2(1400,500)));
                            
                            break;
                    }
                }
            }
            
        }
    }
}
