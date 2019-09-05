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
    public class WorldObjectHandling
    {
        private Scene scene;
        private List<WorldObject> worldObjects;
        private List<Cargo> cargos;
        private Random random;
        private TimeSpan Timer;
        private GameTime Timer2;
        public WorldObjectHandling(Scene scene, List<WorldObject> worldObjects)
        {
            this.worldObjects = worldObjects;
            this.scene = scene;
            cargos = scene.GetCargos();
            random = new Random();

            for (int i = 0; i < 15; i++)
            {
                Vector2 Center = new Vector2(random.Next(-200, 4000), random.Next(-200, 1200));
                if(Center.Y>480 && Center.Y < 620)
                {
                    Center.Y += 140;
                }
                if (random.Next(0, 2) == 0)
                {
                    scene.addCactus(Center);
                }
                else
                {
                    scene.addRock(Center);
                }

            }
            Timer = new TimeSpan(0,0,0,0,1);
            Timer2 = new GameTime();
        }

        //Values must be outside [-300,2220] [-300,1380]
        public void Update(GameTime gameTime)
        {
            if(Timer.TotalMilliseconds>=500)
            {
                Timer.Subtract(new TimeSpan(0,0,0,0,500));
                foreach (WorldObject worldObject in worldObjects)
                {
                    if (worldObject.Hitbox.Center.X < cargos[0].Hitbox.Center.X - 2000)
                    {
                        Vector2 Center = new Vector2(cargos[0].Hitbox.Center.X + 1500 + random.Next(0, 200), random.Next(-200, 1300));
                        if (Center.Y > 480 && Center.Y < 620)
                        {
                            Center.Y += 140;
                        }
                        worldObject.SetPosition(Center);
                        if(worldObject.GetType()== typeof(Cactus))
                        {
                            (worldObject as Cactus).isActivated = false;
                        }
                    }
                }
            }
            
            //Timer.Add(gameTime.ElapsedGameTime);
            Timer += gameTime.ElapsedGameTime;
        }
    }
}
