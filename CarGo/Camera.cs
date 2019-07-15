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
    class Camera
    {
        private Vector2 positon;
        private SpriteBatch spriteBatch;
        private Vector2 screenCenter;
        private Vector2 screenSize;
        private Vector2 offset;
        public Camera(SpriteBatch spriteBatchInit)
        {
            positon =new Vector2(0,0);
            spriteBatch = spriteBatchInit;
            offset = new Vector2(0, 0);
            screenSize = new Vector2(1920, 1080);
            screenCenter = new Vector2(screenSize.X / 2, screenSize.Y / 2);
        }

        public void Update(Cargo cargo,List<Player> players)
        {
            List<Vector2> centers=new List<Vector2>();
            centers.Add(cargo.Hitbox.Center);
            foreach (Player player in players)
            {
                centers.Add(player.Hitbox.Center);
            }
            float sumX = 0;
            float sumY = 0;
            foreach (Vector2 vector in centers)
            {
                sumX += vector.X;
                sumY += vector.Y;
            }
            float borderX = 300;
            float borderY = 300;
            offset.X = sumX / centers.Count -screenCenter.X;
            if (offset.X - cargo.Hitbox.Center.X + screenCenter.X > (screenSize.X-screenCenter.X)-borderX) offset.X = cargo.Hitbox.Center.X + (screenSize.X - screenCenter.X) - borderX - screenCenter.X;
            if (offset.X - cargo.Hitbox.Center.X + screenCenter.X < -((screenSize.X - screenCenter.X) - borderX)) offset.X = cargo.Hitbox.Center.X - ((screenSize.X - screenCenter.X) - borderX) - screenCenter.X;
            offset.Y = sumY / centers.Count - screenCenter.Y;
            if (offset.Y - cargo.Hitbox.Center.Y + screenCenter.Y > ((screenSize.Y-screenCenter.Y)-borderY)) offset.Y = cargo.Hitbox.Center.Y + ((screenSize.Y - screenCenter.Y) - borderY) - screenCenter.Y;
            if (offset.Y - cargo.Hitbox.Center.Y + screenCenter.Y < -((screenSize.Y - screenCenter.Y) - borderY)) offset.Y = cargo.Hitbox.Center.Y - ((screenSize.Y - screenCenter.Y) - borderY) - screenCenter.Y;

        }
        public void Draw(List<Entity> entities,GameTime gameTime)
        {
            spriteBatch.Begin();
            foreach (Entity entity in entities)
            {
                entity.Draw(gameTime, spriteBatch, offset);
            }
            spriteBatch.End();
        }

    }
}
