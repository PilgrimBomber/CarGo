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
        public Dictionary<int, Entity> entitiesID;
        private List<Entity> deadEntities;
        private List<Player> players;
        public List<Player> localPlayers;
        private List<BaseEnemy> enemies;
        private List<WorldObject> worldObjects;
        private List<Cargo> cargos;
        private List<ActiveAbility> activeAbilities;
        private ContentManager content;
        private LevelControl levelControl;
        private WorldObjectHandling worldObjectHandling;
        private Tilemap tilemap;
        private Network.NetworkThread networkThread;
        //private SpriteBatch spriteBatch;
        public Scene(SpriteBatch spriteBatch, ContentManager content, Vector2 screenSize, Game1 game)
        {
            entities = new List<Entity>();
            entitiesID = new Dictionary<int, Entity>();
            deadEntities = new List<Entity>();
            players = new List<Player>();
            localPlayers = new List<Player>();
            enemies = new List<BaseEnemy>();
            worldObjects = new List<WorldObject>();
            cargos = new List<Cargo>();
            activeAbilities = new List<ActiveAbility>();
            camera = new Camera(spriteBatch, screenSize, cargos, players, enemies, worldObjects, activeAbilities);
            this.game = game;
            networkThread = game.networkThread;
            //SoundCollection.Instance.LoadSounds(content);
            //textureCollection = new TextureCollection(content);
            //TextureCollection.Instance.loadTextures(content);
            collisionCheck = new CollisionCheck(cargos, players, enemies, worldObjects, activeAbilities);
            
            worldObjectHandling = new WorldObjectHandling(this, worldObjects);
            tilemap = new Tilemap(1, content);
            
            //enemyAI = new EnemyAI(tilemap, enemies, cargo);
            enemyAI = new EnemyAI(worldObjects, enemies, cargos);
            this.content = content;
            
        }

        public void LoadLevel()
        {
            if(!StateMachine.Instance.networkGame ||networkThread.isMainClient) levelControl = new LevelControl(this, content, cargos, players.Count);
        }


        public void Reset()
        {
            entities = new List<Entity>();
            entitiesID = new Dictionary<int, Entity>();
            deadEntities = new List<Entity>();
            players = new List<Player>();
            enemies = new List<BaseEnemy>();
            worldObjects = new List<WorldObject>();
            cargos = new List<Cargo>();
            activeAbilities = new List<ActiveAbility>();
            collisionCheck = new CollisionCheck(cargos, players, enemies, worldObjects, activeAbilities);
            camera.Reset(cargos,players,enemies,worldObjects,activeAbilities);
            if(!StateMachine.Instance.networkGame || networkThread.isMainClient) worldObjectHandling = new WorldObjectHandling(this, worldObjects);
            tilemap = new Tilemap(1, content);
            //enemyAI = new EnemyAI(tilemap, enemies, cargo);
            enemyAI = new EnemyAI(worldObjects, enemies, cargos);
        }


        private void RemoveDeadEntities()
        {
            foreach (Entity entity in deadEntities)
            {
                if (players.Contains(entity)) removePlayer(entity as Player,true);
                if (enemies.Contains(entity)) removeEnemy(entity as BaseEnemy, true);
                if (worldObjects.Contains(entity)) removeWorldObject(entity as WorldObject,true);
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
            
            
            if(!StateMachine.Instance.networkGame|| networkThread.isMainClient)
            {
                levelControl.Update();
                worldObjectHandling.Update(gameTime);
            }
            enemyAI.Update(gameTime);
            camera.Update();
        }

        public void Draw(GameTime gameTime)
        {
            camera.Draw(gameTime, tilemap);
        }

        public void RemoteUpdatePosition(int objectID,Vector2 center, float rotation, Vector2 velocity)
        {
            if(entitiesID.ContainsKey(objectID))
            {
                entitiesID[objectID].RemoteUpdatePosition(center, rotation, velocity);
            }
            else
            {
                //error, object not existing
            }
        }

        public void RemoteAddPlayer(int clientID, Vector2 center, int objectID, CarType carType, CarFrontType carFront, AbilityType abilityType, OnlinePlayer onlinePlayer)
        {
            addPlayer(false, center, carType, carFront, abilityType, objectID, onlinePlayer);
        }

        public void RemoteAddEntity(EntityType type, Vector2 center, int objectID)
        {
            switch (type)
            {
                case EntityType.Player:
                    //
                    break;
                case EntityType.Cargo:
                    addCargo(center, objectID,false);
                    break;
                case EntityType.EnemySlow:
                    addEnemy(EnemyType.EnemyDummy, center, objectID,false);
                    break;
                case EntityType.EnemyFast:
                    addEnemy(EnemyType.EnemyFast, center, objectID,false);
                    break;
                case EntityType.Cactus:
                    addCactus(center, objectID,false);
                    break;
                case EntityType.Rock:
                    addRock(center, objectID,false);
                    break;
                case EntityType.Skull:
                    addSkull(center, objectID,false);
                    break;
                default:
                    break;
            }
        }

        public void RemoteRemoveEntity(int objectID)
        {
            if (!entitiesID.ContainsKey(objectID)) return;
            Entity entity = entitiesID[objectID];
            if (players.Contains(entity)) removePlayer(entity as Player, false);
            if (enemies.Contains(entity)) removeEnemy(entity as BaseEnemy, false);
            if (worldObjects.Contains(entity)) removeWorldObject(entity as WorldObject,false);
        }

        public void RemoteUpdateHitpoints(int objectID, int newHitpoints)
        {
            entitiesID[objectID].hitpoints = newHitpoints;
        }

        public void addCactus(Vector2 center, int objectID, bool local)
        {
            Cactus cactus = new Cactus(this, center, objectID);
            addEntity(cactus,local);
            worldObjects.Add(cactus);
        }

        public void addRock(Vector2 center, int objectID, bool local)
        {
            Rock rock = new Rock(this, center, objectID);
            addEntity(rock,local);
            worldObjects.Add(rock);
        }
        public void addSkull(Vector2 center, int objectID, bool local)
        {
            Skull skull = new Skull(this, center, objectID);
            addEntity(skull,local);
            worldObjects.Add(skull);
        }

        public void removeWorldObject(WorldObject worldObject, bool local)
        {
            worldObjects.Remove(worldObject);
            removeEntity(worldObject,local);
        }

        public void addEnemy(EnemyType enemyType, Vector2 center, int objectID, bool local)
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
            addEntity(enemy,local);
        }

        public void removeEnemy(BaseEnemy enemy, bool local)
        {
            enemies.Remove(enemy);
            removeEntity(enemy,local);
        }


        public void addPlayer(bool local, Vector2 center, CarType carType, CarFrontType carFrontType, AbilityType abilityType, int objectID, OnlinePlayer onlinePlayer)
        {
            Player player = new Player(onlinePlayer.clientID == ID_Manager.Instance.ClientNumber, this, center, carType, carFrontType, abilityType, objectID, onlinePlayer);
            players.Add(player);
            addEntity(player,local);
            if (onlinePlayer.clientID == ID_Manager.Instance.ClientNumber)
            {
                localPlayers.Add(player);
            }
        }

        public void removePlayer(Player player, bool local)
        {
            players.Remove(player);
            removeEntity(player,local);
        }

        public void addCargo(Vector2 center, int objectID, bool local)
        {
            Cargo cargo = new Cargo(this, center, objectID);
            cargos.Add(cargo);
            addEntity(cargo, local);
            
        }
        
        public List<Cargo> GetCargos()
        {
            return cargos;
        }

        private void addEntity(Entity entity, bool local)
        {
            entities.Add(entity);
            if(entity.objectID>0) entitiesID.Add(entity.objectID, entity);
            if (local && StateMachine.Instance.networkGame)
            {
                if (entity.entityType == EntityType.Player) networkThread.BroadCastEntityUpdate(Network.ObjectMessageType.PlayerSpawn, entity);
                else networkThread.BroadCastEntityUpdate(Network.ObjectMessageType.Spawn, entity);
            }

        }

        private void removeEntity(Entity entity, bool local)
        {
            entities.Remove(entity);
            entitiesID.Remove(entity.objectID);
            if (local && StateMachine.Instance.networkGame)
            {
                networkThread.BroadCastEntityUpdate(Network.ObjectMessageType.Despawn, entity);
            }
        }
        
        public void addActiveAbility(ActiveAbility activeAbility, bool local)
        {
            activeAbilities.Add(activeAbility);
            addEntity(activeAbility, local);
        }
        
        public void removeActiveAbility(ActiveAbility activeAbility, bool local)
        {
            activeAbilities.Remove(activeAbility);
            removeEntity(activeAbility, local);
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
