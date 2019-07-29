using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

namespace CarGo
{

    interface ICollidable
    {
        void Collide(Entity entity, EntityCategory entityCategory);
    }

    interface IUpdateable
    {
        void Update();
    }

    public enum EntityCategory { Player, Cargo, Enemy, WorldObject}


    public abstract class Entity: ICollidable,IUpdateable//, IDrawable
    {
        protected RotRectangle hitbox;
        protected int hitpoints;
        protected Texture2D texture;
        protected Vector2 velocity;
        protected Scene scene;
        public bool noCollision;
        public RotRectangle Hitbox { get => hitbox;}
        public Vector2 Velocity { get => velocity; set => velocity = value; }
        


        //public event EventHandler<EventArgs> DrawOrderChanged;
        //public event EventHandler<EventArgs> VisibleChanged;
        //int IDrawable.DrawOrder => throw new NotImplementedException();
        //bool IDrawable.Visible => throw new NotImplementedException();

        public abstract void Collide(Entity entity, EntityCategory entityCategory);
        public abstract void Update();
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 offset);
        public abstract void GetPushed(Vector2 direction);
        public abstract void TakeDamage(int damage);
    }
}
