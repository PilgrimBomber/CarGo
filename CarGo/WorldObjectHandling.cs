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

        public List<RotRectangle> spawnPositons;
        public WorldObjectHandling(Scene scene, List<WorldObject> worldObjects)
        {
            spawnPositons = new List<RotRectangle>();
            for (int i = 0; i<4; i++)
            {
                spawnPositons.Add(new RotRectangle(0, new Vector2(400 + (int)i * 100, 400),new Vector2(100,100)));
                  
            }
            spawnPositons.Add(new RotRectangle(90, new Vector2(960, 540), new Vector2(100, 100)));

            this.worldObjects = worldObjects;
            this.scene = scene;
            cargos = scene.GetCargos();
            random = new Random();

            for (int i = 0; i < 15; i++)
            {
                //Vector2 Center = new Vector2(random.Next(-200, 4000), random.Next(-200, 1200));
                //if(Center.Y>480 && Center.Y < 620)
                //{
                //    Center.Y += 140;
                //}
                
                Vector2 Center = new Vector2(random.Next(-200, 4000), random.Next(-200, 1200));
                if (Center.Y > 480 && Center.Y < 620)
                {
                    Center.Y += 140;
                }
                int number = random.Next(0, 11);
                if (number == 0)
                {
                    scene.addSkull(Center, ID_Manager.Instance.GetID());
                }
                else if (number%2 == 0)
                {
                    scene.addCactus(Center, ID_Manager.Instance.GetID());
                }else
                {
                    scene.addRock(Center, ID_Manager.Instance.GetID());
                }

                while (CheckCollisions(worldObjects.Last(), worldObjects)&& CheckCollisions(worldObjects.Last().Hitbox))
                {
                    Center = new Vector2(random.Next(-200, 4000), random.Next(-200, 1200));
                    if (Center.Y > 480 && Center.Y < 620)
                    {
                        Center.Y += 140;
                    }
                    worldObjects.Last().Hitbox.SetPosition(Center);
                }
            }
            Timer = new TimeSpan(0,0,0,0,1);
        }

        //Values must be outside [-300,2220] [-300,1380]
        public void Update(GameTime gameTime)
        {
            if(Timer.TotalMilliseconds>=500)
            {
                Timer=Timer.Subtract(new TimeSpan(0,0,0,0,500));
                foreach (WorldObject worldObject in worldObjects)
                {
                    if (worldObject.Hitbox.Center.X < cargos[0].Hitbox.Center.X - 2000)
                    {
                        do
                        {
                            Vector2 Center = new Vector2(cargos[0].Hitbox.Center.X + 1800 + random.Next(-200, 200), random.Next(-200, 1300));
                            if (Center.Y > 480 && Center.Y < 620)
                            {
                                Center.Y += 140;
                            }
                            worldObject.Hitbox.SetPosition(Center);
                        } while (CheckCollisions(worldObject, worldObjects));
                        
                        
                        
                        if(worldObject.GetType()== typeof(Cactus))
                        {
                            (worldObject as Cactus).isActivated = false;
                            (worldObject as Cactus).isExploded= false;
                        }

                        if (worldObject.GetType() == typeof(Skull))
                        {
                            (worldObject as Skull).isActivated = false;
                        }
                    }
                }
            }
            
            Timer += gameTime.ElapsedGameTime;
        }

        private bool CheckCollisions(WorldObject worldObject1, List<WorldObject> worldObjects)
        {
            if (worldObjects.Count == 0) return false;
            foreach (WorldObject worldObject in worldObjects)
            {
                if (worldObject.Equals(worldObject1)) continue;
                if (CollisionCheck.CheckCollision(worldObject1.Hitbox, worldObject.Hitbox))
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckCollisions(RotRectangle hitbox)
        {
            //if (worldObjects.Count == 0) return false;
            foreach (RotRectangle playerSpawnPosition in spawnPositons)
            {
                if (CollisionCheck.CheckCollision(hitbox, playerSpawnPosition))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
