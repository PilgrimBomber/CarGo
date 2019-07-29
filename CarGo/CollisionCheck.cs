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
    public class CollisionCheck
    {
        List<Cargo> cargos;
        List<Player> players;
        List<BaseEnemy> enemies;
        List<WorldObject> worldObjects;
        public CollisionCheck(List<Cargo> cargos, List<Player> players, List<BaseEnemy> enemies, List<WorldObject> worldObjects)
        {
            this.cargos = cargos;
            this.players = players;
            this.enemies = enemies;
            this.worldObjects = worldObjects;
        }
        public void CheckCollisons(List<Entity> entities)
        {
            for (int i = 0; i <entities.Count-1; i++)
            {
                for (int j = i+1; j < entities.Count; j++)
                {
                    if(CheckCollision(entities[i].Hitbox,entities[j].Hitbox))
                    {
                        entities[i].Collide(entities[j], EntityType.Player);
                        entities[j].Collide(entities[i], EntityType.Player);
                    }
                }
            }
        }

        public void CheckCollisions()
        {
            //Player
            for (int i=0; i<players.Count;i++)
            {
                //Player - Player
                for (int j = i+1; j < players.Count; j++)
                {
                    if(CheckCollision(players[i],players[j]))
                    {
                        players[i].Collide(players[j], EntityType.Player);
                        players[j].Collide(players[i], EntityType.Player);
                    }  
                }

                //Player - Cargos
                for (int j = 0; j < cargos.Count; j++)
                {
                    if (CheckCollision(players[i], cargos[j]))
                    {
                        players[i].Collide(cargos[j], EntityType.Cargo);
                        cargos[j].Collide(players[i], EntityType.Player);
                    }
                }
                //Player - Enemies
                for (int j = 0; j < enemies.Count; j++)
                {
                    if (CheckCollision(players[i], enemies[j]))
                    {
                        players[i].Collide(enemies[j], EntityType.Enemy);
                        enemies[j].Collide(players[i], EntityType.Player);
                    }
                }
                //Player - WorldObject
                for (int j = 0; j < worldObjects.Count; j++)
                {
                    if (CheckCollision(players[i], worldObjects[j]))
                    {
                        players[i].Collide(worldObjects[j], EntityType.WorldObject);
                        worldObjects[j].Collide(players[i], EntityType.Player);
                    }
                }
            }

            //Cargo
            for (int i = 0; i < cargos.Count; i++)
            {
                //Cargo - Cargo
                for (int j = i+1;j<cargos.Count ; j++)
                {
                    if (CheckCollision(cargos[i], cargos[j]))
                    {
                        cargos[i].Collide(cargos[j], EntityType.Cargo);
                        cargos[j].Collide(cargos[i], EntityType.Cargo);
                    }
                }

                //Cargo - Enemy
                for (int j = 0; j < enemies.Count; j++)
                {
                    if (CheckCollision(cargos[i], enemies[j]))
                    {
                        cargos[i].Collide(enemies[j], EntityType.Enemy);
                        enemies[j].Collide(cargos[i], EntityType.Cargo);
                    }
                }

                ////Cargo - WorldObject
                //for (int j = 0; j < worldObjects.Count; j++)
                //{
                //    if (CheckCollision(cargos[i], worldObjects[j]))
                //    {
                //        cargos[i].Collide(worldObjects[j], EntityType.WorldObject);
                //        worldObjects[j].Collide(cargos[i], EntityType.Cargo);
                //    }
                //}
            }

            //Enemy
            for (int i =0; i<enemies.Count; i++)
            {
                //Enemy - Enemy
                for (int j = 0; j < enemies.Count; j++)
                {
                    if (CheckCollision(enemies[i], enemies[j]))
                    {
                        enemies[i].Collide(enemies[j], EntityType.Enemy);
                        enemies[j].Collide(enemies[i], EntityType.Enemy);
                    }
                }
                //Enemy - WorldObject
                for (int j = 0; j < worldObjects.Count; j++)
                {
                    if (CheckCollision(enemies[i], worldObjects[j]))
                    {
                        enemies[i].Collide(worldObjects[j], EntityType.WorldObject);
                        worldObjects[j].Collide(enemies[i], EntityType.Enemy);
                    }
                }
            }
        }

        

        public static bool CheckCollision(Entity e1, Entity e2)
        {
            return CheckCollision(e1.Hitbox, e2.Hitbox);
        }
        public static bool CheckCollision(RotRectangle r1,RotRectangle r2)
        {
            //Check if they are not in range of each other
            if(r1.Offset.Length()+r2.Offset.Length()<(r1.Center-r2.Center).Length())
                return false;
            else
            {
                //If they are in range of each other
                //Use the seperating axis theorem (SAT) to check if they collide
                Vector2 r1Axis1 = r1.Corners[3] - r1.Corners[0];
                Vector2 r1Axis2 = r1.Corners[1] - r1.Corners[0];
                Vector2 r2Axis1 = r2.Corners[3] - r2.Corners[0];
                Vector2 r2Axis2 = r2.Corners[1] - r2.Corners[0];

                //for each axis
                //project the Corners of both rectangles on the axis and check if there is a gap between them
                //if there is a gap then there is no colission
                List<float> valuesR1=new List<float>();
                List<float> valuesR2 = new List<float>();
                //r1Axis1
                for (int i = 0; i < 4; i++)
                {
                    valuesR1.Add(Vector2.Dot(r1.Corners[i], r1Axis1) / r1Axis1.LengthSquared());
                }
                for (int i = 0; i < 4; i++)
                {
                    valuesR2.Add(Vector2.Dot(r2.Corners[i], r1Axis1) / r1Axis1.LengthSquared());
                }
                if (valuesR1.Min() > valuesR2.Max() || valuesR1.Max() < valuesR2.Min())
                    return false;
                //r1Axis2
                valuesR1.Clear();
                valuesR2.Clear();
                for (int i = 0; i < 4; i++)
                {
                    valuesR1.Add(Vector2.Dot(r1.Corners[i], r1Axis2) / r1Axis2.LengthSquared());
                }
                for (int i = 0; i < 4; i++)
                {
                    valuesR2.Add(Vector2.Dot(r2.Corners[i], r1Axis2) / r1Axis2.LengthSquared());
                }
                if (valuesR1.Min() > valuesR2.Max() || valuesR1.Max() < valuesR2.Min())
                    return false;

                //r2Axis1
                valuesR1.Clear();
                valuesR2.Clear();
                for (int i = 0; i < 4; i++)
                {
                    valuesR1.Add(Vector2.Dot(r1.Corners[i], r2Axis1) / r2Axis1.LengthSquared());
                }
                for (int i = 0; i < 4; i++)
                {
                    valuesR2.Add(Vector2.Dot(r2.Corners[i], r2Axis1) / r2Axis1.LengthSquared());
                }
                if (valuesR1.Min() > valuesR2.Max() || valuesR1.Max() < valuesR2.Min())
                    return false;

                //r2Axis2
                valuesR1.Clear();
                valuesR2.Clear();
                for (int i = 0; i < 4; i++)
                {
                    valuesR1.Add(Vector2.Dot(r1.Corners[i], r2Axis2) / r2Axis2.LengthSquared());
                }
                for (int i = 0; i < 4; i++)
                {
                    valuesR2.Add(Vector2.Dot(r2.Corners[i], r2Axis2) / r2Axis2.LengthSquared());
                }
                if (valuesR1.Min() > valuesR2.Max() || valuesR1.Max() < valuesR2.Min())
                    return false;


                //if there is no gap on any axe the rectangles collide
                return true;
            }
        }

        public static bool CheckCollision(Vector2 position, Vector2 direction, RotRectangle rotRectangle)
        {
            
            //Vector2 direction = target - position;
            float angle = (float)Math.Atan(direction.Y / direction.X);
            RotRectangle line = new RotRectangle(0, position + direction / 2, new Vector2(direction.Length() / 2,25));
            line.Rotate(angle);
            return CheckCollision(rotRectangle, line);
        }
        //public static bool CheckCollision(Vector2 position, Vector2 direction, ref float distance, RotRectangle obstacle)
        //{

        //    //Vector2 direction = target - position;
        //    float angle = (float)Math.Atan(direction.Y / direction.X);
        //    RotRectangle line = new RotRectangle(0, position + direction / 2, new Vector2(direction.Length() / 2, 10));
        //    line.Rotate(angle);

        //    return CheckCollision(obstacle, line);
        //}
    }
}
