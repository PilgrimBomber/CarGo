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
        private List<Cargo> cargos;
        private List<Player> players;
        private List<BaseEnemy> enemies;
        private List<WorldObject> worldObjects;
        private List<ActiveAbility> activeAbilities;
        private HUD hud;

        public Camera(SpriteBatch spriteBatchInit, Vector2 screenSize, List<Cargo> cargos, List<Player> players, List<BaseEnemy> enemies, List<WorldObject> worldObjects, List<ActiveAbility> activeAbilities)
        {
            hud = new HUD(spriteBatchInit, players, cargos, activeAbilities, screenSize);
            positon =new Vector2(0,0);
            spriteBatch = spriteBatchInit;
            offset = new Vector2(0, 0);
            this.screenSize = screenSize;
            screenCenter = new Vector2(screenSize.X / 2, screenSize.Y / 2);
            this.cargos = cargos;
            this.players = players;
            this.enemies = enemies;
            this.worldObjects = worldObjects;
            this.activeAbilities = activeAbilities;
        }

        public void Update()
        {
            hud.Update();
            List<Vector2> centers=new List<Vector2>();
            foreach (Cargo cargo in cargos)
            {
                centers.Add(cargo.Hitbox.Center);
            }
            foreach (Player player in players)
            {
                centers.Add(player.Hitbox.Center);
            }
            float sumX = 0f;
            float sumY = 0f;
            foreach (Vector2 vector in centers)
            {
                sumX += vector.X;
                sumY += vector.Y;
            }
            float borderX = 300f;
            float borderY = 300f;
            offset.X = sumX / centers.Count -screenCenter.X;
            if (offset.X - cargos[0].Hitbox.Center.X + screenCenter.X > (screenSize.X-screenCenter.X)-borderX) offset.X = cargos[0].Hitbox.Center.X + (screenSize.X - screenCenter.X) - borderX - screenCenter.X;
            if (offset.X - cargos[0].Hitbox.Center.X + screenCenter.X < -((screenSize.X - screenCenter.X) - borderX)) offset.X = cargos[0].Hitbox.Center.X - ((screenSize.X - screenCenter.X) - borderX) - screenCenter.X;
            offset.Y = sumY / centers.Count - screenCenter.Y;
            if (offset.Y - cargos[0].Hitbox.Center.Y + screenCenter.Y > ((screenSize.Y-screenCenter.Y)-borderY)) offset.Y = cargos[0].Hitbox.Center.Y + ((screenSize.Y - screenCenter.Y) - borderY) - screenCenter.Y;
            if (offset.Y - cargos[0].Hitbox.Center.Y + screenCenter.Y < -((screenSize.Y - screenCenter.Y) - borderY)) offset.Y = cargos[0].Hitbox.Center.Y - ((screenSize.Y - screenCenter.Y) - borderY) - screenCenter.Y;
        }
        public void Draw(GameTime gameTime, Tilemap tilemap)
        {
            spriteBatch.Begin();
            tilemap.Draw(gameTime, spriteBatch, offset);
            foreach (WorldObject worldObject in worldObjects)
            {
                if (worldObject.Hitbox.Center.X - offset.X > -200 && worldObject.Hitbox.Center.X - offset.X < screenSize.X + 200
                    && worldObject.Hitbox.Center.Y - offset.Y > -200 && worldObject.Hitbox.Center.Y < screenSize.Y + offset.Y + 200)
                    worldObject.Draw(gameTime, spriteBatch, offset);
            }
            foreach (Cargo cargo in cargos)
            {
                if (cargo.Hitbox.Center.X - offset.X > -200 && cargo.Hitbox.Center.X - offset.X < screenSize.X + 200
                    && cargo.Hitbox.Center.Y - offset.Y > -200 && cargo.Hitbox.Center.Y < screenSize.Y + offset.Y + 200)
                    cargo.Draw(gameTime, spriteBatch, offset);
            }
            foreach (BaseEnemy enemy in enemies)
            {
                if (enemy.Hitbox.Center.X - offset.X > -200 && enemy.Hitbox.Center.X - offset.X < screenSize.X + 200
                    && enemy.Hitbox.Center.Y - offset.Y > -200 && enemy.Hitbox.Center.Y < screenSize.Y + offset.Y + 200)
                    enemy.Draw(gameTime, spriteBatch, offset);
            }
            foreach (ActiveAbility activeAbility in activeAbilities)
            {
                if (activeAbility.Hitbox.Center.X - offset.X > -200 && activeAbility.Hitbox.Center.X - offset.X < screenSize.X + 200
                    && activeAbility.Hitbox.Center.Y - offset.Y > -200 && activeAbility.Hitbox.Center.Y < screenSize.Y + offset.Y + 200)
                    activeAbility.Draw(gameTime, spriteBatch, offset);
            }
            foreach (Player player in players)
            {
                if (player.Hitbox.Center.X - offset.X > -200 && player.Hitbox.Center.X - offset.X < screenSize.X + 200
                    && player.Hitbox.Center.Y - offset.Y > -200 && player.Hitbox.Center.Y < screenSize.Y + offset.Y + 200)
                    player.Draw(gameTime, spriteBatch, offset);
            }
            hud.Draw(spriteBatch);
            spriteBatch.End();
        }

    }
}
