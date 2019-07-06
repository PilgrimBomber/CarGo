﻿using System;
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
        void Collide(Entity entity);
    }

    interface IUpdateable
    {
        void Update();
    }

    public abstract class Entity: ICollidable,IUpdateable//, IDrawable
    {
        protected RotRectangle hitbox;
        protected int hitpoints;
        protected Texture2D texture;

        public RotRectangle Hitbox { get => hitbox; set => hitbox = value; }
        //public event EventHandler<EventArgs> DrawOrderChanged;
        //public event EventHandler<EventArgs> VisibleChanged;
        //int IDrawable.DrawOrder => throw new NotImplementedException();
        //bool IDrawable.Visible => throw new NotImplementedException();

        public abstract void Collide(Entity entity);
        public abstract void Update();
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);


        public abstract void GetPushed(Vector2 direction);
    }
}
