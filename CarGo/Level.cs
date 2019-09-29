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

    public abstract class Level
    {
        public int levelNumber;
        protected Scene scene;
        //protected ContentManager content;
        protected Vector2 startPosition;
        protected List<Cargo> cargos;
        protected List<SpawnEvent> spawnEvents;

        public Level()
        {

        }

        protected void HandleStartSpawn()
        {
            List<SpawnEvent> EventsHappened = new List<SpawnEvent>();
            foreach (SpawnEvent spawn in spawnEvents)
            {
                if (spawn.GetType() == typeof(StartSpawnEvent))
                {
                    spawn.SpawnAll(0);
                    EventsHappened.Add(spawn);

                }
            }
            if (EventsHappened.Count > 0)
            {
                foreach (SpawnEvent eventsHappened in EventsHappened)
                {
                    spawnEvents.Remove(eventsHappened);
                }
            }
        }

        public void Update()
        {
            int distanceTravelled = 0;
            List<SpawnEvent> EventsHappened = new List<SpawnEvent>();


            if (cargos.Count > 0)
            {
                distanceTravelled = (int)(cargos.First().Hitbox.Center - startPosition).Length();
            }
            else
            {
                scene.addCargo(new Vector2(1920 / 2, 1080 / 2));
                startPosition = cargos.First().Hitbox.Center;
            }
            
            foreach (SpawnEvent spawnEvent in spawnEvents)
            {
                if (spawnEvent.CheckDistance(distanceTravelled))
                {
                    spawnEvent.SpawnAll(distanceTravelled);
                    EventsHappened.Add(spawnEvent);

                }
            }
            //remove Events
            if (EventsHappened.Count > 0)
            {
                foreach (SpawnEvent eventsHappened in EventsHappened)
                {
                    spawnEvents.Remove(eventsHappened);
                }
            }

        }
    }



    public class SpawnEvent
    {

        protected List<EntityType> types;
        protected List<Vector2> offsets;
        protected int triggerDistance;
        protected Scene scene;
        public SpawnEvent(int triggerDistance, Scene scene)
        {
            types = new List<EntityType>();
            offsets = new List<Vector2>();
            this.triggerDistance = triggerDistance;
            this.scene = scene;
        }

        /// <summary>
        /// Adds an entity to an SpawnEvent
        /// Relative Position must be outside [-1620,3540] [-780,1860]
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="relativePositionX"></param>
        /// <param name="relativePositionY"></param>
        public virtual void AddSpawn(EntityType entityType, int relativePositionX, int relativePositionY)
        {
            if (entityType == EntityType.Cargo || entityType == EntityType.Player) throw new ArgumentOutOfRangeException();
            types.Add(entityType);
            if (relativePositionX > -1620 && relativePositionX < 3540 && relativePositionY > -780 && relativePositionY < 1860) throw new ArgumentOutOfRangeException();

            offsets.Add(new Vector2(relativePositionX, relativePositionY));
        }

        public virtual void AddSpawn(EntityType entityType, int entityAmount)
        {
            Random random = new Random();
            Vector2 position;
            //Values must be outside [-300,2220] [-300,1380]
            if (entityType == EntityType.Cargo || entityType == EntityType.Player) throw new ArgumentOutOfRangeException();
            

            for (int i = 0; i < entityAmount; i++)
            {
                types.Add(entityType);
                switch (random.Next(1, 12))
                {
                    default:
                        position = new Vector2();
                        break;
                    case 1:
                        position = new Vector2(
                            random.Next(-2470, 80),
                            random.Next(-1380,-780));
                        break;
                    case 2:
                        position = new Vector2(
                            random.Next(80, 1840),
                            random.Next(-1380, -780));
                        break;
                    case 3:
                        position = new Vector2(
                            random.Next(1840, 3540),
                            random.Next(-1380, -780));
                        break;
                    case 4:
                        position = new Vector2(
                            random.Next(3540, 5240),
                            random.Next(-1380, -780));
                        break;
                    case 5:
                        position = new Vector2(
                            random.Next(3540,5240),
                            random.Next(-780,540));
                        break;
                    case 6:
                        position = new Vector2(
                            random.Next(3540,5240),
                            random.Next(540,1860));
                        break;
                    case 7:
                        position = new Vector2(
                            random.Next(3540,5240),
                            random.Next(1860,2460));
                        break;
                    case 8:
                        position = new Vector2(
                            random.Next(1840,3540),
                            random.Next(1860, 2460));
                        break;
                    case 9:
                        position = new Vector2(
                            random.Next(80,1840),
                            random.Next(1860, 2460));
                        break;
                    case 10:
                        position = new Vector2(
                            random.Next(-2470,80),
                            random.Next(1860, 2460));
                        break;
                    case 11:
                        position = new Vector2(
                            random.Next(-2470,-1620),
                            random.Next(-780,1860));
                        break;
                }
                offsets.Add(position);
            }           
        }

        public virtual void SpawnAll(int distanceTravelled)
        {
            for (int i = 0; i < types.Count; i++)
            {

                Vector2 Position = new Vector2(offsets[i].X + distanceTravelled, offsets[i].Y);
                switch (types[i])
                {
                    //case EntityType.Cactus: scene.addCactus(Position); break;
                    case EntityType.EnemyDummy: scene.addEnemy(EnemyType.EnemyDummy, Position); break;
                    case EntityType.EnemyFast: scene.addEnemy(EnemyType.EnemyFast, Position); break;
                    //case EntityType.Rock: scene.addRock(Position); break;
                }

            }
        }

        public bool CheckDistance(int distanceTravelled)
        {
            return distanceTravelled > triggerDistance;
        }
    }

    public class StartSpawnEvent:SpawnEvent
    {
        private int numPlayers;
        public StartSpawnEvent(Scene scene, int numPlayers):base(0,scene)
        {
            this.numPlayers = numPlayers;
        }

        public override void AddSpawn(EntityType entityType, int relativePositionX, int relativePositionY)
        {
            types.Add(entityType);
            offsets.Add(new Vector2(relativePositionX, relativePositionY));
        }

        public void AddPlayer(int relativePositionX, int relativePositionY)
        {

        }

        public override void SpawnAll(int distanceTravelled)
        {
            for (int i = 0; i < types.Count; i++)
            {

                Vector2 Position = new Vector2(offsets[i].X + distanceTravelled, offsets[i].Y);
                switch (types[i])
                {
                    case EntityType.Cactus: scene.addCactus(Position); break;
                    case EntityType.Skull: scene.addSkull(Position); break;
                    case EntityType.EnemyDummy: scene.addEnemy(EnemyType.EnemyDummy, Position); break;
                    case EntityType.EnemyFast: scene.addEnemy(EnemyType.EnemyFast, Position); break;
                    case EntityType.Rock: scene.addRock(Position); break;
                    case EntityType.Player: scene.addPlayer(PlayerIndex.One, Position,CarType.Medium,CarFrontType.Bumper,AbilityType.RocketLauncher); break;
                }

            }
        }
    }


    public class Level1:Level
    {
        
        public Level1(Scene scene, ContentManager content, List<Cargo> cargos)
        {
            this.scene = scene;
            //this.content = content;
            this.cargos = cargos;
            levelNumber = 1;
            spawnEvents = new List<SpawnEvent>();

            spawnEvents.Add(new StartSpawnEvent(scene, 1));
            spawnEvents[0].AddSpawn(EntityType.Cargo, 960, 540);

            spawnEvents.Add(new SpawnEvent(0, scene));
            spawnEvents[1].AddSpawn(EntityType.EnemyFast,2);
            spawnEvents[1].AddSpawn(EntityType.EnemyDummy, 5);

            spawnEvents.Add(new SpawnEvent(500, scene));
            spawnEvents[2].AddSpawn(EntityType.EnemyDummy,2);

            spawnEvents.Add(new SpawnEvent(1000, scene));
            spawnEvents[3].AddSpawn(EntityType.EnemyDummy, 2);

            spawnEvents.Add(new SpawnEvent(1500, scene));
            spawnEvents[4].AddSpawn(EntityType.EnemyDummy, 2);

            spawnEvents.Add(new SpawnEvent(3000, scene));
            spawnEvents[5].AddSpawn(EntityType.EnemyDummy, 5);

            spawnEvents.Add(new SpawnEvent(5000, scene));
            spawnEvents[6].AddSpawn(EntityType.EnemyDummy, 4);

            spawnEvents.Add(new SpawnEvent(8000, scene));
            spawnEvents[7].AddSpawn(EntityType.EnemyDummy, 4);

            HandleStartSpawn();
        }

        
    }

    
}
