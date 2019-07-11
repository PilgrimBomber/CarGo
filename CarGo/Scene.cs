﻿using System;
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
    public class Scene: IUpdateable
    {
        private Camera camera;
        private CollisionCheck collisionCheck;
        private List<Entity> entities;
        private List<Player> players;
        private List<BaseEnemy> enemies;
        private List<WorldObject> worldObjects;
        private Cargo cargo;
        private ContentManager content;

        //private SpriteBatch spriteBatch;
        public Scene(SpriteBatch spriteBatch, ContentManager content)
        {
            entities = new List<Entity>();
            players = new List<Player>();
            enemies = new List<BaseEnemy>();
            worldObjects = new List<WorldObject>();
            camera = new Camera(spriteBatch);
            collisionCheck = new CollisionCheck();
            this.content = content;
        }

        public void addWorldObject(WorldObject worldObject)
        {
            worldObjects.Add(worldObject);
            addEntity(worldObject);
        }

        public void addEnemy(BaseEnemy enemy)
        {
            enemies.Add(enemy);
            addEntity(enemy);
        }

        public void addPlayer(Player player)
        {
            players.Add(player);
            addEntity(player);
        }
        public void addCargo(Cargo cargo)
        {
            if (this.cargo == null) this.cargo = cargo;
            entities.Add(cargo);
        }

        private void addEntity(Entity entity)
        {
            entities.Add(entity);
        }

        

        public void Update()
        {
            collisionCheck.CheckCollisons(entities);
            foreach (Entity entity in entities)
            {
                entity.Update();

            }
        }

        public void Draw(GameTime gameTime)
        {
            camera.Draw(entities, gameTime);
        }

        
    }
}
