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
        private Texture2D cargoLifeBar;

        public HUD(SpriteBatch spriteBatch, List<Player> players, List<Cargo> cargos, List<ActiveAbility> activeAbilities, Vector2 screenSize)
        {
            this.players = players;
            this.cargos = cargos;
            this.activeAbilities = activeAbilities;
            this.screenSize = screenSize;
            graphicsDevice = spriteBatch.GraphicsDevice;
            cargoLifeBar = new Texture2D(graphicsDevice, 1, 1);
        }

        public void Update()
        {
            cargoLifeBar = createLifebar(cargoLifeBar, 1380, 20, cargos[0].getPercentLife(), 2);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(cargoLifeBar, new Vector2 (300 ,30), Color.White );
        }

        private Texture2D createLifebar(Texture2D texture, int width, int height, float percentLife)
        {
            return createLifebar(texture ,width, height, percentLife, 1);
        }

        private Texture2D createLifebar(Texture2D lifeBar, int width, int height, float percentLife, int borderThickness)
        {
            if (lifeBar == null) lifeBar = new Texture2D(graphicsDevice, width, height);
            else if(lifeBar.Width != width || lifeBar.Height != height) lifeBar = new Texture2D(graphicsDevice, width, height);
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
