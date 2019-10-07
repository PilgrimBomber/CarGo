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

    public class EnemyAI
    {
        //private AStar aStar;
        List<BaseEnemy> enemies;
        Cargo cargo;
        List<Cargo> cargos;
        List<WorldObject> worldObjects;

        private int updateCounter = 0;
        //public EnemyAI(Tilemap tilemap, List<BaseEnemy> enemies,List<Cargo> cargos)
        //{
        //    //aStar = new AStar(tilemap);
        //    this.enemies = enemies;
        //    this.cargos = cargos;
        //    usingAStar = true;
        //}

        public EnemyAI(List<WorldObject> worldObjects, List<BaseEnemy> enemies, List<Cargo> cargos)
        {
            this.worldObjects = worldObjects;
            this.enemies = enemies;
            this.cargos = cargos;
        }

        public void Update(GameTime gameTime)
        {
            if (cargo == null) cargo = cargos.First();


            //Direct Move
            if (updateCounter == 0)
            {

                foreach (BaseEnemy baseEnemy in enemies)
                {
                    List<Vector2> path = new List<Vector2>();
                    path.Clear();
                    path.Add(baseEnemy.Hitbox.Center);
                    Search(baseEnemy.Hitbox.Center, baseEnemy.Hitbox.Offset.X, cargo.Hitbox.Center, path);
                    baseEnemy.Path = path;
                    //baseEnemy.Velocity /= baseEnemy.Velocity.Length();
                    if (baseEnemy.Hitbox.Center.X < cargo.Hitbox.Center.X)
                    {
                        baseEnemy.Hitbox.RotationRad = (float)Math.Atan((cargo.Hitbox.Center - baseEnemy.Hitbox.Center).Y / (cargo.Hitbox.Center - baseEnemy.Hitbox.Center).X) + Geometry.DegToRad(90);
                    }
                    else
                    {
                        baseEnemy.Hitbox.RotationRad = (float)Math.Atan((cargo.Hitbox.Center - baseEnemy.Hitbox.Center).Y / (cargo.Hitbox.Center - baseEnemy.Hitbox.Center).X) - Geometry.DegToRad(90);
                    }
                }
            }

            updateCounter++;
            if (updateCounter > 10)
            {
                updateCounter = 0;
            }

        }


        private void Search(Vector2 position, float pathWidth, Vector2 target, List<Vector2> path)
        {
            if (path.Count > 5) return;

            Vector2 direction = target - position;
            float angle = Geometry.DegToRad(1);
            while (true)
            {
                float distance = 0;
                int collisionCount = 0;
                float shortestDistance = 0;// int.MaxValue;
                foreach (WorldObject worldObject in worldObjects)
                {

                    if (CollisionCheck.CheckCollision(position, direction, pathWidth, worldObject.Hitbox))
                    {

                        distance = Vector2.Distance(position, worldObject.Hitbox.Center);
                        if (collisionCount == 0)
                        {
                            shortestDistance = distance;
                        }

                        if (distance < shortestDistance)
                        {
                            shortestDistance = distance;
                        }
                        collisionCount++;
                    }
                }
                if (collisionCount > 0)
                {
                    direction.Normalize();
                    direction *= shortestDistance;
                    direction = Geometry.Rotate(direction, angle);
                    angle *= -1.2f;
                    if (angle > Math.PI) return;
                }
                else
                {
                    position += direction;
                    path.Add(position);
                    if (Vector2.Distance(position, target) < 10) return;
                    Search(position, pathWidth, target, path);
                    return;
                }
            }
        }

    }  
}
