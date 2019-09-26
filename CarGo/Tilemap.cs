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
    

    public class Tilemap
    {

        private const int constWidth = 64;
        private const int constHeight = 64;
        private const int mapWidth = 500;
        private const int mapHeight = 25;
        private int[,] tilemap;
        private List<Texture2D> textures;
        //public static readonly Location[] DIRS = new[]
        //{
        //    new Location(1, 0), // to right of tile
        //    new Location(0, -1), // below tile
        //    new Location(-1, 0), // to left of tile
        //    new Location(0, 1), // above tile
        //    new Location(1, 1), // diagonal top right
        //    new Location(-1, 1), // diagonal top left
        //    new Location(1, -1), // diagonal bottom right
        //    new Location(-1, -1) // diagonal bottom left
        //};

        
        public Tilemap(int levelNumber, ContentManager content)
        {
            tilemap = new int[mapWidth,mapHeight];
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
                    
                }
            }
            
            //for (int i = 0; i < tilemap.GetLength(0); i++)
            //{
            //    tilemap[i, 11] = random.Next() % 3 + 12;
            //    tilemap[i, 12] = random.Next() % 3 + 15;
            //    tilemap[i, 13] = random.Next() % 3 + 18;
            //}
        }

        //returns the grid coordinates for input world coordinates
        //public static Location CoordinatesWorldToGrid(float X, float Y)
        //{
        //    return new Location((int)(X / constWidth), (int)(Y / constHeight));
        //}
        //public static Location CoordinatesWorldToGrid(Entity entity)
        //{
        //    return new Location((int)(entity.Hitbox.Center.X / constWidth),(int)(entity.Hitbox.Center.Y/ constHeight));
        //}

        ////returns the world coordinates for input grid coordinates
        //public static Vector2 CoordinatesGridToWorld(int X, int Y)
        //{
        //    return new Vector2(X*constWidth +constWidth/2, Y* constHeight + constHeight/2);
        //}
        //public static Vector2 CoordinatesGridToWorld(Location location)
        //{
        //    return new Vector2(location.x * constWidth + constWidth / 2, location.y * constHeight + constHeight / 2);
        //}

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 offset)
        {
            int minY = (int)(offset.Y +240)/constHeight;
            int minX = (int)(offset.X + 660)/constWidth;
            if (minX >= 500 - 32) minX = 500 - 32;
            if (minY > mapHeight - 18) minY = mapHeight - 18;
            if (minY < 0) minY = 0;

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
