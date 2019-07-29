﻿using System;
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
            eventTime[1] = 4;
        }

        public override void Update(TimeSpan timer)
        {
            for (int i = 0; i < 10; i++)
            {
                if (timer.TotalSeconds >= eventTime[i] && !eventTriggered[i])
                {
                    switch(i)
                    {
                        case 0:
                            eventTriggered[0] = true;
                            scene.addCargo(new Vector2(1920 / 2, 1080 / 2));
                            scene.addEnemy(EnemyType.EnemyDummy,new Vector2(1200,800));
                            scene.addEnemy(EnemyType.EnemyDummy,new Vector2(1250,750));
                            scene.addEnemy(EnemyType.EnemyDummy, new Vector2(200, 600));
                            scene.addCactus(new Vector2 (1200,50));
                            scene.addRock(new Vector2(1200, 700));
                            scene.addRock(new Vector2(1250, 700));
                            scene.addRock(new Vector2(1150, 700));
                            break;
                        case 1:
                            eventTriggered[1] = true;
                            scene.addEnemy(EnemyType.EnemyDummy, new Vector2(3000, 800));
                            scene.addEnemy(EnemyType.EnemyDummy, new Vector2(3300, 200));
                            scene.addEnemy(EnemyType.EnemyDummy, new Vector2(3600, 800));
                            scene.addEnemy(EnemyType.EnemyDummy, new Vector2(3800, 200));
                            scene.addEnemy(EnemyType.EnemyDummy, new Vector2(2200, 800));
                            scene.addEnemy(EnemyType.EnemyDummy, new Vector2(2300, 200));
                            scene.addEnemy(EnemyType.EnemyDummy, new Vector2(2400, 800));
                            scene.addEnemy(EnemyType.EnemyDummy, new Vector2(2500, 200));
                            scene.addEnemy(EnemyType.EnemyDummy, new Vector2(2600, 800));
                            scene.addEnemy(EnemyType.EnemyDummy, new Vector2(2700, 200));
                            break;
                    }
                }
            }
            
        }
    }
}
