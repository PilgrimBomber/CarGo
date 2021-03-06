﻿using System;
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
        void Update(GameTime gameTime);
    }

    public enum EntityCategory { Player, Cargo, Enemy, WorldObject, ActiveAbility}
    public enum EntityType { Player,Cargo, EnemySlow, EnemyFast, Cactus, Rock, Skull, Ability}

    public abstract class Entity: ICollidable,IUpdateable//, IDrawable
    {
        protected RotRectangle hitbox;
        public int hitpoints;
        protected int maxLife;
        protected Texture2D texture;
        protected Vector2 velocity;
        protected Scene scene;
        public bool noCollision;
        public RotRectangle Hitbox { get => hitbox;}
        public Vector2 Velocity { get => velocity; set => velocity = value; }

        public int objectID;

        public EntityType entityType;

        //public event EventHandler<EventArgs> DrawOrderChanged;
        //public event EventHandler<EventArgs> VisibleChanged;
        //int IDrawable.DrawOrder => throw new NotImplementedException();
        //bool IDrawable.Visible => throw new NotImplementedException();

        public abstract void Collide(Entity entity, EntityCategory entityCategory);
        public abstract void Update(GameTime gameTime);
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 offset)
        {
            spriteBatch.Draw(texture, hitbox.Center - offset, null, Color.White, hitbox.RotationRad, hitbox.Offset, 1.0f, SpriteEffects.None, 0f);
        }
        public abstract void GetPushed(Vector2 direction);
        public abstract void TakeDamage(int damage);

        public abstract void UpdateVolume();
        public float getPercentLife()
        {
            return ((float)hitpoints / maxLife) * 100f;
        }
        

        public virtual void RemoteUpdatePosition(Vector2 center, float rotation, Vector2 velocity)
        {
            hitbox.SetPosition(center);
            hitbox.SetRotation(rotation);
            this.velocity = velocity;
        }
    }
}
