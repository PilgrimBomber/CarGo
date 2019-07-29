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
        private Camera camera;
        private CollisionCheck collisionCheck;
        private EnemyAI enemyAI;
        private List<Entity> entities;
        private List<Entity> deadEntities;
        private List<Player> players;
        private List<BaseEnemy> enemies;
        private List<WorldObject> worldObjects;
        private List<Cargo> cargos;
        private ContentManager content;
        private LevelControl levelControl;
        private Tilemap tilemap;
        private SoundCollection soundCollection;
        private TextureCollection textureCollection;
        //private SpriteBatch spriteBatch;
        public Scene(SpriteBatch spriteBatch, ContentManager content, Vector2 screenSize)
        {
            entities = new List<Entity>();
            deadEntities = new List<Entity>();
            players = new List<Player>();
            enemies = new List<BaseEnemy>();
            worldObjects = new List<WorldObject>();
            cargos = new List<Cargo>();

            camera = new Camera(spriteBatch,screenSize);
            collisionCheck = new CollisionCheck(cargos,players,enemies,worldObjects);
            levelControl = new LevelControl(this,content);
            tilemap = new Tilemap(1, content);
            soundCollection = new SoundCollection(content);
            textureCollection = new TextureCollection(content);
            //enemyAI = new EnemyAI(tilemap, enemies, cargo);
            enemyAI = new EnemyAI(worldObjects, enemies, cargos);
            this.content = content;

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
                entity.Update();
            }
            RemoveDeadEntities();
            levelControl.Update(gameTime);
            enemyAI.Update();
            camera.Update(cargos.First(), players);

        }

        public void Draw(GameTime gameTime)
        {
            camera.Draw(entities, gameTime, tilemap);
        }




        public void addCactus(Vector2 center)
        {
            int indexX = (int)(center.X / 64);
            int indexY = (int)(center.Y / 64);
            tilemap.SetCollisionMap(indexX, indexY, CollisionType.Slow);
            Cactus cactus = new Cactus(soundCollection, textureCollection, this, new Vector2(indexX * 64 + 32, indexY * 64 + 32));
            cactus.indexX = indexX;
            cactus.indexY = indexY;
            addEntity(cactus);
            worldObjects.Add(cactus);
        }

        public void addRock(Vector2 center)
        {
            int indexX = (int)(center.X / 64);
            int indexY = (int)(center.Y / 64);
            tilemap.SetCollisionMap(indexX, indexY, CollisionType.staticCollision);
            Rock rock = new Rock(soundCollection, textureCollection, this, new Vector2(indexX * 64 + 32, indexY * 64 + 32));
            rock.indexX = indexX;
            rock.indexY = indexY;
            addEntity(rock);
            worldObjects.Add(rock);
        }

        public void removeWorldObject(WorldObject worldObject)
        {
            worldObjects.Remove(worldObject);
            removeEntity(worldObject);
            tilemap.SetCollisionMap(worldObject.indexX, worldObject.indexY, CollisionType.noCollision);
        }

        public void addEnemy(EnemyType enemyType, Vector2 center)
        {
            BaseEnemy enemy;
            switch (enemyType)
            {
                case EnemyType.EnemyDummy: enemy = new EnemyDummy(soundCollection, textureCollection, this, center);
                    break;
                default: enemy = new EnemyDummy(soundCollection, textureCollection, this, center);
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

        public void addPlayer(PlayerIndex playerIndex, Vector2 center, CarType carType, CarFrontType carFrontType, AbilityType abilityType)
        {
            Player player = new Player(soundCollection, textureCollection, this, playerIndex, center, carType, carFrontType, abilityType);
            players.Add(player);
            addEntity(player);
        }
        public void removePlayer(Player player)
        {
            players.Remove(player);
            removeEntity(player);
        }

        public void addCargo(Vector2 center)
        {
            Cargo cargo = new Cargo(soundCollection, textureCollection, this, center);
            cargos.Add(cargo);
            entities.Add(cargo);
        }
        
        private void addEntity(Entity entity)
        {
            entities.Add(entity);
        }

        private void removeEntity(Entity entity)
        {
            entities.Remove(entity);
        }
        

        

        
    }
}
