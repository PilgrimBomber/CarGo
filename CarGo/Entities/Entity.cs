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

    interface ICollidable
    {
        void Collide();
    }

    interface IUpdateable
    {
        void Update();
    }

    public abstract class Entity: ICollidable,IUpdateable//, IDrawable
    {
        protected Rectangle hitbox;
        protected int hitpoints;
        protected Texture2D texture;
        protected int orientation;
        //public event EventHandler<EventArgs> DrawOrderChanged;
        //public event EventHandler<EventArgs> VisibleChanged;
        //int IDrawable.DrawOrder => throw new NotImplementedException();
        //bool IDrawable.Visible => throw new NotImplementedException();

        public abstract void Collide();
        public abstract void Update();
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
