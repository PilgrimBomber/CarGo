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
    public enum EnemyType{EnemyDummy, EnemyFast}

    public abstract class BaseEnemy:Entity
    {
        
        public bool wasPushed;
        protected List<Vector2> path;
        protected List<Cargo> cargos;
        protected float baseSpeed;
        protected int Damage;
        protected Animation animation;
        public List<Vector2> Path { get => path; set => path = value; }
        

        public BaseEnemy(Scene scene)
        {
            this.scene = scene;
            cargos = scene.GetCargos();
            velocity *= 0f;
            wasPushed = false;
            
        }

        public override void Update(GameTime gameTime)
        {
            if (hitpoints <= 0) scene.KillEntity(this);
            FollowPath();
            //Move the Dummy
            hitbox.Move(velocity);

            //Slows the dummy over time
            if (wasPushed)
            {
                velocity *= 0.96f;
                if (velocity.Length() < 0.05)
                {
                    velocity *= 0;
                    wasPushed = false;
                }
            }

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 offset)
        {
            if (wasPushed) spriteBatch.Draw(texture, hitbox.Center - offset, null, Color.White, hitbox.RotationRad, hitbox.Offset, 1.0f, SpriteEffects.None, 0f);
            else animation.Draw(gameTime, spriteBatch, offset);
        }

        protected void FollowPath()
        {
            if (path != null)
            {
                if (path.Count == 0) return;
                if (Vector2.Distance(path.First(), hitbox.Center) < 10)
                {
                    if (wasPushed) return;
                    path.RemoveAt(0);
                    if (path.Count == 0) return;
                    velocity = path.First() - this.hitbox.Center;
                    velocity.Normalize();
                    if(hitbox.Center.X<cargos[0].Hitbox.Center.X)
                    {
                        velocity *= baseSpeed * 1.3f;
                    }
                    else
                    {
                        velocity *= baseSpeed* 0.8f;
                    }

                }

            }
        }
    }
}
