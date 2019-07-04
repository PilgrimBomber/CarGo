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
    public class Scene: IUpdateable
    {
          private Camera camera;
        private List<Entity> entities;

        public Scene()
        {
            entities = new List<Entity>();
            camera = new Camera();
        }


        public void addEntity(Entity entity)
        {
            entities.Add(entity);
        }
        public void Update()
        {
            //foreach(Entity entity in entities)
            //{
            //    entity.Update();
                
            //}
        }

        public void Draw(GameTime gameTime)
        {

            camera.Draw(entities, gameTime);
        }

        
    }
}
