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
        protected bool[] eventTriggered;
        protected int[] eventTime;
        protected int[] triggerdistance;

        public virtual void Update(TimeSpan timer)
        {
            throw new NotImplementedException();
        }
    }
    class Level1:Level
    {
        
        private Vector2 startPosition;
        private static int numEvents=10;
        private List<Cargo> cargos;
        public Level1(Scene scene, ContentManager content, List<Cargo> cargos)
        {
            this.scene = scene;
            this.content = content;
            this.cargos = cargos;
            levelNumber = 1;
            eventTriggered = new bool[numEvents];
            eventTime = new int[numEvents];
            triggerdistance = new int[numEvents];
            eventTime[0] = 0;
            eventTime[1] = 4;
            triggerdistance[0] = 0;
            triggerdistance[1] = 200;
        }

        public override void Update(TimeSpan timer)
        {
            int distanceTravelled=0;
            if (cargos.Count>0)
            {
                distanceTravelled = (int)(cargos.First().Hitbox.Center - startPosition).Length();
            }
            else
            {
                scene.addCargo(new Vector2(1920 / 2, 1080 / 2));
                startPosition = cargos.First().Hitbox.Center;
            }
            

            for (int i = 0; i < numEvents; i++)
            {
                //if (timer.TotalSeconds >= eventTime[i] && !eventTriggered[i])
                if(distanceTravelled >= triggerdistance[i] && !eventTriggered[i])
                {
                    switch(i)
                    {
                        case 0:
                            eventTriggered[0] = true;
                            
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
                            scene.addEnemy(EnemyType.EnemyDummy, new Vector2(3000, 540));
                            scene.addEnemy(EnemyType.EnemyDummy, new Vector2(3300, 540));
                            scene.addEnemy(EnemyType.EnemyDummy, new Vector2(3600, 520));
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
