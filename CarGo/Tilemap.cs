using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace CarGo
{
    public enum CollisionType {noCollision, staticCollision, Slow }

    public class Tilemap
    {

        private const int constWidth = 64;
        private const int constHeight = 64;
        private int[,] tilemap;
        private CollisionType[,] collisionMap;
        private List<Texture2D> textures;

        public Tilemap(int levelNumber, ContentManager content)
        {
            tilemap = new int[500, 25];
            collisionMap = new CollisionType[500, 25];
            textures = new List<Texture2D>();
            textures.Add(content.Load<Texture2D>("textures/Tile_0"));
            textures.Add(content.Load<Texture2D>("textures/Enemy_Dummy"));
            generateMap();

        }

        private void generateMap()
        {
            Random random = new Random();
            for (int x = 0; x < tilemap.GetLength(1); x++)
            {
                for (int y = 0; y < tilemap.GetLength(0); y++)
                {
                    tilemap[y, x] = 0 ;//aus datei oder random
                    collisionMap[y, x] = CollisionType.noCollision;
                }
            }
            
            //for (int i = 0; i < tilemap.GetLength(0); i++)
            //{
            //    tilemap[i, 11] = random.Next() % 3 + 12;
            //    tilemap[i, 12] = random.Next() % 3 + 15;
            //    tilemap[i, 13] = random.Next() % 3 + 18;
            //}
        }

        public void SetCollisionMap(int indexX, int indexY, CollisionType collisionType)
        {
            collisionMap[indexX, indexY] = collisionType;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 offset)
        {
            int minY = (int)(offset.Y +240)/constHeight;
            int minX = (int)(offset.X + 660)/constWidth;
            if (minX >= 500 - 32) minX = 500 - 32;
            
            for (int i = minY; i < minY+18; i++) 
            {
                for (int j = minX; j < minX+32; j++) 
                {
                    spriteBatch.Draw(textures[tilemap[j, i]], new Vector2(constWidth * (j-11)- offset.X, constHeight * (i-4) - offset.Y), Color.White);
                }

            }
        }
    }



}
