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
    public class HUD
    {
        private Camera camera;
        private List<Player> players;
        private List<Cargo> cargos;
        private List<BaseEnemy> enemies;
        private List<ActiveAbility> activeAbilities;
        private GraphicsDevice graphicsDevice;
        private Vector2 screenSize;

        public HUD(SpriteBatch spriteBatch, List<Player> players, List<Cargo> cargos, List<ActiveAbility> activeAbilities, Vector2 screenSize)
        {
            this.players = players;
            this.cargos = cargos;
            this.activeAbilities = activeAbilities;
            this.screenSize = screenSize;
            graphicsDevice = spriteBatch.GraphicsDevice;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(createLifebar(1380, 20, cargos[0].getPercentLife(), 2), new Vector2 (300 ,30), Color.White );
        }

        private Texture2D createLifebar(int width, int height, float percentLife)
        {
            return createLifebar(width, height, percentLife, 1);
        }

        private Texture2D createLifebar(int width, int height, float percentLife, int borderThickness)
        {
            Texture2D lifeBar = new Texture2D(graphicsDevice, width, height);
            Color[] data = new Color[width * height];
            int redPart = (int)((width - borderThickness * 2) * percentLife / 100);

            for (int i = 0; i < borderThickness; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    data[width * i + j] = Color.Black;
                }
            }
            for (int i = borderThickness; i < height - borderThickness; i++)
            {
                for (int j = 0; j < borderThickness; j++)
                {
                    data[width * i + j] = Color.Black;
                }
                for (int j = borderThickness; j < borderThickness + redPart + 1; j++)
                {
                    data[width * i + j] = Color.Red;
                }
                for (int j = redPart + 2; j < width - borderThickness; j++)
                {
                    data[width * i + j] = Color.DarkGray;
                }
                for (int j = width - borderThickness; j < width; j++)
                {
                    data[width * i + j] = Color.Black;
                }
            }
            for (int i = height - borderThickness; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    data[width * i + j] = Color.Black;
                }
            }

            lifeBar.SetData(data);
            return lifeBar;
        }        
    }
}
