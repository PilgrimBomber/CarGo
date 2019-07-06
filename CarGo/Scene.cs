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
    public class Scene: IUpdateable
    {
        private Camera camera;
        private CollisionCheck collisionCheck;
        private List<Entity> entities;
        private ContentManager content;

        //private SpriteBatch spriteBatch;
        public Scene(SpriteBatch spriteBatch, ContentManager content)
        {
            entities = new List<Entity>();
            camera = new Camera(spriteBatch);
            collisionCheck = new CollisionCheck();
            this.content = content;
        }

        public void addEntity(Entity entity)
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
