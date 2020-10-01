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
    public class Scene
    {
        private Game1 game;
        private Camera camera;
        private CollisionCheck collisionCheck;
        private EnemyAI enemyAI;
        public List<Entity> entities;
        private List<Entity> deadEntities;
        private List<Player> players;
        private List<BaseEnemy> enemies;
        private List<WorldObject> worldObjects;
        private List<Cargo> cargos;
        private List<ActiveAbility> activeAbilities;
        private ContentManager content;
        private LevelControl levelControl;
        private WorldObjectHandling worldObjectHandling;
        private Tilemap tilemap;

        //private SpriteBatch spriteBatch;
        public Scene(SpriteBatch spriteBatch, ContentManager content, Vector2 screenSize, Game1 game)
        {
            entities = new List<Entity>();
            deadEntities = new List<Entity>();
            players = new List<Player>();
            enemies = new List<BaseEnemy>();
            worldObjects = new List<WorldObject>();
            cargos = new List<Cargo>();
            activeAbilities = new List<ActiveAbility>();

            //SoundCollection.Instance.LoadSounds(content);
            //textureCollection = new TextureCollection(content);
            //TextureCollection.Instance.loadTextures(content);
            collisionCheck = new CollisionCheck(cargos, players, enemies, worldObjects, activeAbilities);
            camera = new Camera(spriteBatch, screenSize, cargos, players, enemies, worldObjects, activeAbilities);
            worldObjectHandling = new WorldObjectHandling(this, worldObjects);
            tilemap = new Tilemap(1, content);
            this.game = game;
            //enemyAI = new EnemyAI(tilemap, enemies, cargo);
            enemyAI = new EnemyAI(worldObjects, enemies, cargos);
            this.content = content;
            
        }

        public void LoadLevel()
        {
            levelControl = new LevelControl(this, content, cargos, players.Count);
        }


        public void Reset()
        {
            entities = new List<Entity>();
            deadEntities = new List<Entity>();
            players = new List<Player>();
            enemies = new List<BaseEnemy>();
            worldObjects = new List<WorldObject>();
            cargos = new List<Cargo>();
            activeAbilities = new List<ActiveAbility>();
            collisionCheck = new CollisionCheck(cargos, players, enemies, worldObjects, activeAbilities);
            camera.Reset(cargos,players,enemies,worldObjects,activeAbilities);
            worldObjectHandling = new WorldObjectHandling(this, worldObjects);
            tilemap = new Tilemap(1, content);
            //enemyAI = new EnemyAI(tilemap, enemies, cargo);
            enemyAI = new EnemyAI(worldObjects, enemies, cargos);
        }


        private void RemoveDeadEntities()
        {
            foreach (Entity entity in deadEntities)
            {
                if (players.Contains(entity)) removePlayer(entity as Player);
                if (enemies.Contains(entity)) removeEnemy(entity as BaseEnemy);
                if (worldObjects.Contains(entity)) removeWorldObject(entity as WorldObject);
            }
            deadEntities.Clear();
        }

        public void KillEntity(Entity entity)
        {
            deadEntities.Add(entity);
        }

        public void Update(GameTime gameTime)
        {
            //collisionCheck.CheckCollisons(entities);
            collisionCheck.CheckCollisions();
            foreach (Entity entity in entities)
            {
                entity.Update(gameTime);
            }
            RemoveDeadEntities();
            levelControl.Update();
            worldObjectHandling.Update(gameTime);
            enemyAI.Update(gameTime);
            camera.Update();

        }

        public void Draw(GameTime gameTime)
        {
            camera.Draw(gameTime, tilemap);
        }




        public void addCactus(Vector2 center, int objectID)
        {
            Cactus cactus = new Cactus(this, center, objectID);
            addEntity(cactus);
            worldObjects.Add(cactus);
        }

        public void addRock(Vector2 center, int objectID)
        {
            Rock rock = new Rock(this, center, objectID);
            addEntity(rock);
            worldObjects.Add(rock);
        }
        public void addSkull(Vector2 center, int objectID)
        {
            Skull skull = new Skull(this, center, objectID);
            addEntity(skull);
            worldObjects.Add(skull);
        }

        public void removeWorldObject(WorldObject worldObject)
        {
            worldObjects.Remove(worldObject);
            removeEntity(worldObject);
        }

        public void addEnemy(EnemyType enemyType, Vector2 center, int objectID)
        {
            BaseEnemy enemy;
            switch (enemyType)
            {
                case EnemyType.EnemyDummy: enemy = new EnemySlow(this, center, objectID);
                    break;
                case EnemyType.EnemyFast: enemy = new EnemyFast(this, center, objectID);
                    break;
                default: enemy = new EnemySlow(this, center, objectID);
                    break;
            }
            enemies.Add(enemy);
            addEntity(enemy);
        }

        public void removeEnemy(BaseEnemy enemy)
        {
            enemies.Remove(enemy);
            removeEntity(enemy);
        }


        public void addPlayer(bool local, Vector2 center, CarType carType, CarFrontType carFrontType, AbilityType abilityType, int objectID)
        {
            Player player = new Player(local, this, PlayerIndex.One, center, carType, carFrontType, abilityType, objectID);
            players.Add(player);
            addEntity(player);
        }

        public void addPlayer(PlayerIndex playerIndex, Vector2 center, CarType carType, CarFrontType carFrontType, AbilityType abilityType, int objectID)
        {
            Player player = new Player(true, this, playerIndex, center, carType, carFrontType, abilityType, objectID);
            players.Add(player);
            addEntity(player);
        }
        public void removePlayer(Player player)
        {
            players.Remove(player);
            removeEntity(player);
        }

        public void addCargo(Vector2 center, int objectID)
        {
            Cargo cargo = new Cargo(this, center, objectID);
            cargos.Add(cargo);
            entities.Add(cargo);
        }
        
        public List<Cargo> GetCargos()
        {
            return cargos;
        }

        private void addEntity(Entity entity)
        {
            entities.Add(entity);
        }

        private void removeEntity(Entity entity)
        {
            entities.Remove(entity);
        }
        
        public void addActiveAbility(ActiveAbility activeAbility)
        {
            activeAbilities.Add(activeAbility);
            addEntity(activeAbility);
        }
        
        public void removeActiveAbility(ActiveAbility activeAbility)
        {
            activeAbilities.Remove(activeAbility);
            removeEntity(activeAbility);
        }

        public void Finish(bool won)
        {
            if (won) StateMachine.Instance.ChangeState(GameState.MenuWon);
            else StateMachine.Instance.ChangeState(GameState.MenuLost);


        }

        public void UpdateAllVolumes()
        {
            foreach( Entity entity in entities)
            {
                entity.UpdateVolume();
            }
        }

    }
}
