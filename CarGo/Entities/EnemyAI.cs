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
        private bool usingAStar;
        List<BaseEnemy> enemies;
        Cargo cargo;
        List<Cargo> cargos;
        List<WorldObject> worldObjects;

        private int updateCounter=0;
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
            usingAStar = false;
        }

        public void Update(GameTime gameTime)
        {
            if(cargo==null)cargo = cargos.First();
            
            
            //Grid-Based Version
            if(usingAStar)
            {
                throw new NotImplementedException();
                if (updateCounter == 0)
                {
                    foreach (BaseEnemy enemy in enemies)
                    {
                        if (enemy.GetType() == typeof(EnemyDummy))
                        {
                            //(enemy as EnemyDummy).Path2 = aStar.FindPath(Tilemap.CoordinatesWorldToGrid(enemy), Tilemap.CoordinatesWorldToGrid(cargo));
                        }
                    }
                }
                updateCounter++;
                if (updateCounter > 100)
                {
                    updateCounter = 0;
                }
            }
            
            else
            {
                //Direct Move
                if (updateCounter == 0)
                {
                    //    foreach (BaseEnemy baseEnemy in enemies)
                    //    {
                    //        if (baseEnemy.wasPushed) continue;
                    //        int collisionCount = 0;
                    //        Vector2 direction = cargo.Hitbox.Center - baseEnemy.Hitbox.Center;
                    //        foreach (WorldObject worldObject in worldObjects)
                    //        {
                    //            if (CollisionCheck.CheckCollision(baseEnemy.Hitbox.Center, cargo.Hitbox.Center - baseEnemy.Hitbox.Center, worldObject.Hitbox))
                    //            {
                    //                collisionCount++;
                    //            }
                    //        }
                    //        if (collisionCount > 0)
                    //        {


                    //            baseEnemy.Velocity *= 0;
                    //        }
                    //        else
                    //        {
                    //            baseEnemy.Velocity = (cargo.Hitbox.Center - baseEnemy.Hitbox.Center);
                    //            baseEnemy.Velocity /= baseEnemy.Velocity.Length();

                    //            //baseEnemy.Hitbox.Rotate((float)Math.Atan((cargo.Hitbox.Center - baseEnemy.Hitbox.Center).Y / (cargo.Hitbox.Center - baseEnemy.Hitbox.Center).X));
                    //            if(baseEnemy.Hitbox.Center.X < cargo.Hitbox.Center.X)
                    //            {
                    //                baseEnemy.Hitbox.RotationRad = (float)Math.Atan((cargo.Hitbox.Center - baseEnemy.Hitbox.Center).Y / (cargo.Hitbox.Center - baseEnemy.Hitbox.Center).X) + Geometry.DegToRad(90);
                    //                baseEnemy.Velocity *= 2.2f;
                    //            }
                    //            else
                    //            {
                    //                baseEnemy.Hitbox.RotationRad = (float)Math.Atan((cargo.Hitbox.Center - baseEnemy.Hitbox.Center).Y / (cargo.Hitbox.Center - baseEnemy.Hitbox.Center).X) - Geometry.DegToRad(90);
                    //                baseEnemy.Velocity *= 1.8f;
                    //            }
                    //        }
                    //    }
                    
                    foreach(BaseEnemy baseEnemy in enemies)
                    {
                        List<Vector2> path = new List<Vector2>();
                        path.Clear();
                        path.Add(baseEnemy.Hitbox.Center);
                        Search(baseEnemy.Hitbox.Center, cargo.Hitbox.Center, path);
                        baseEnemy.Path = path;
                        //baseEnemy.Velocity /= baseEnemy.Velocity.Length();
                        if (baseEnemy.Hitbox.Center.X < cargo.Hitbox.Center.X)
                        {
                            baseEnemy.Hitbox.RotationRad = (float)Math.Atan((cargo.Hitbox.Center - baseEnemy.Hitbox.Center).Y / (cargo.Hitbox.Center - baseEnemy.Hitbox.Center).X) + Geometry.DegToRad(90);
                            //baseEnemy.Velocity *= 2.2f;
                        }
                        else
                        {
                            baseEnemy.Hitbox.RotationRad = (float)Math.Atan((cargo.Hitbox.Center - baseEnemy.Hitbox.Center).Y / (cargo.Hitbox.Center - baseEnemy.Hitbox.Center).X) - Geometry.DegToRad(90);
                            //baseEnemy.Velocity *= 1.8f;
                        }
                    }


                }

                updateCounter++;
                if (updateCounter > 10)
                {
                    updateCounter = 0;
                }
            }
        }


        private void Search(Vector2 position, Vector2 target, List<Vector2> path)
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
                    
                    if (CollisionCheck.CheckCollision(position, direction, worldObject.Hitbox))
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
                    direction*= shortestDistance;
                    direction = Geometry.Rotate(direction, angle);
                    angle *= -1.2f;
                }
                else
                {
                    position += direction;
                    path.Add(position);
                    if (Vector2.Distance(position, target) < 10) return;
                    Search(position, target, path);
                    return;
                }
            }
        }

        


    }
    //public class AStar
    //{
    //    private Tilemap tilemap;
    //    Location start;
    //    Location target;
    //    //searchable range of the grid
        
    //    public Dictionary<Location, Location> cameFrom = new Dictionary<Location, Location>();
    //    public Dictionary<Location, float> costSoFar = new Dictionary<Location, float>();
    //    public AStar(Tilemap tilemap)
    //    {
    //        this.tilemap = tilemap;
            

            
    //    }

    //    static public float Heuristic(Location a, Location b)
    //    {
    //        return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
    //    }

    //    private void Search(Location start, Location target)
    //    {
    //        this.start = start;
    //        this.target = target;
    //        cameFrom.Clear();
    //        costSoFar.Clear();
    //        var frontier = new PriorityQueue<Location>();
    //        // Add the starting location to the frontier with a priority of 0
    //        frontier.Enqueue(start, 0f);

    //        cameFrom.Add(start, start); // is set to start, None in example
    //        costSoFar.Add(start, 0f);

    //        while (frontier.Count > 0f)
    //        {
    //            // Get the Location from the frontier that has the lowest
    //            // priority, then remove that Location from the frontier
    //            Location current = frontier.Dequeue();

    //            // If we're at the goal Location, stop looking.
    //            if (current.Equals(target)) break;

    //            // Neighbors will return a List of valid tile Locations
    //            // that are next to, diagonal to, above or below current
    //            foreach (var neighbor in tilemap.Neighbors(current))
    //            {

    //                // If neighbor is diagonal to current, graph.Cost(current,neighbor)
    //                // will return Sqrt(2). Otherwise it will return only the cost of
    //                // the neighbor, which depends on its type, as set in the TileType enum.
    //                // So if this is a normal floor tile (1) and it's neighbor is an
    //                // adjacent (not diagonal) floor tile (1), newCost will be 2,
    //                // or if the neighbor is diagonal, 1+Sqrt(2). And that will be the
    //                // value assigned to costSoFar[neighbor] below.
    //                float newCost = costSoFar[current] + tilemap.Cost(current, neighbor);

    //                // If there's no cost assigned to the neighbor yet, or if the new
    //                // cost is lower than the assigned one, add newCost for this neighbor
    //                if (!costSoFar.ContainsKey(neighbor) || newCost < costSoFar[neighbor])
    //                {

    //                    // If we're replacing the previous cost, remove it
    //                    if (costSoFar.ContainsKey(neighbor))
    //                    {
    //                        costSoFar.Remove(neighbor);
    //                        cameFrom.Remove(neighbor);
    //                    }

    //                    costSoFar.Add(neighbor, newCost);
    //                    cameFrom.Add(neighbor, current);
    //                    float priority = newCost + Heuristic(neighbor, target);
    //                    frontier.Enqueue(neighbor, priority);
    //                }
    //            }
    //        }
    //    }
    //    public List<Location> FindPath(Location start, Location target)
    //    {
    //        Search(start, target);
    //        List<Location> path = new List<Location>();
    //        Location current = target;
    //        // path.Add(current);

    //        while (!current.Equals(start))
    //        {
    //            //if (!cameFrom.ContainsKey(current))
    //            //{
    //            //    //MonoBehaviour.print("cameFrom does not contain current.");
    //            //    return new List<Location>();
    //            //}
    //            path.Add(current);
    //            current = cameFrom[current];
    //        }
    //        path.Add(start);
    //        // path.Add(start);
    //        path.Reverse();
    //        return path;
    //    }
    //}


    //public class Location
    //{

    //    public readonly int x, y;

    //    public Location(int x, int y)
    //    {
    //        this.x = x;
    //        this.y = y;
    //    }
    //    public override bool Equals(System.Object obj)
    //    {
    //        Location loc = obj as Location;
    //        return this.x == loc.x && this.y == loc.y;
    //    }

    //    // This is creating collisions. How do I solve this?
    //    public override int GetHashCode()
    //    {
    //        return (x * 597) ^ (y * 1173);
    //    }
    //}

    
    //public class PriorityQueue<T>
    //{
    //    // From Red Blob: I'm using an unsorted array for this example, but ideally this
    //    // would be a binary heap. Find a binary heap class:
    //    // * https://bitbucket.org/BlueRaja/high-speed-priority-queue-for-c/wiki/Home
    //    // * http://visualstudiomagazine.com/articles/2012/11/01/priority-queues-with-c.aspx
    //    // * http://xfleury.github.io/graphsearch.html
    //    // * http://stackoverflow.com/questions/102398/priority-queue-in-net

    //    private List<KeyValuePair<T, float>> elements = new List<KeyValuePair<T, float>>();

    //    public int Count
    //    {
    //        get { return elements.Count; }
    //    }

    //    public void Enqueue(T item, float priority)
    //    {
    //        elements.Add(new KeyValuePair<T, float>(item, priority));
    //    }

    //    // Returns the Location that has the lowest priority
    //    public T Dequeue()
    //    {
    //        int bestIndex = 0;

    //        for (int i = 0; i < elements.Count; i++)
    //        {
    //            if (elements[i].Value < elements[bestIndex].Value)
    //            {
    //                bestIndex = i;
    //            }
    //        }

    //        T bestItem = elements[bestIndex].Key;
    //        elements.RemoveAt(bestIndex);
    //        return bestItem;
    //    }
    //}

    
}
