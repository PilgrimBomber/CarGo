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
    public enum AnimationType { Explosion, Zombie_Slow, Zombie_Fast }

    public class Animation
    {
        private TimeSpan timeExisting;
        private RotRectangle hitbox;
        private AnimationType animationType;
        private bool reapeating;
        private TimeSpan frameTime;
        private int frameCount;
        private int currentFrame;

        private Texture2D spritesheet;
        private Vector2 animationOffset;

        public Animation(AnimationType animationType, RotRectangle hitbox)
        {
            this.animationType = animationType;
            timeExisting = new TimeSpan(0, 0, 0, 0, 0);
            this.hitbox = hitbox;
           

            currentFrame = 0;

            switch (animationType)
            {
                case AnimationType.Explosion:
                    reapeating = false;
                    frameTime = new TimeSpan(0, 0, 0, 0, 100);
                    frameCount = 11;
                    spritesheet = TextureCollection.getInstance().GetTexture(TextureType.Explosion_Animation);
                    break;
                case AnimationType.Zombie_Slow:
                    reapeating = true;
                    frameTime = new TimeSpan(0, 0, 0, 0, 150);
                    frameCount = 8;
                    spritesheet = TextureCollection.getInstance().GetTexture(TextureType.Enemy_Zombie_Slow_Animation);
                    break;
                case AnimationType.Zombie_Fast:
                    reapeating = true;
                    frameTime = new TimeSpan(0, 0, 0, 0, 150);
                    frameCount = 8;
                    spritesheet = TextureCollection.getInstance().GetTexture(TextureType.Enemy_Zombie_Fast_Animation);
                    break;
            }

            animationOffset =new Vector2((spritesheet.Width / frameCount) / 2, spritesheet.Height / 2);


        }


        public bool Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 offset)
        {
            timeExisting += gameTime.ElapsedGameTime;

            if(timeExisting >= frameTime)
            {
                timeExisting -= frameTime;
                currentFrame++;
                if (currentFrame >= frameCount)
                {
                    if(reapeating)
                    {
                        currentFrame = 0;
                      
                    }
                    else
                    {
                        //Animation beenden
                        return false;
                    }
                }
            }

            spriteBatch.Draw(spritesheet, new Rectangle((int)(hitbox.Center.X  - offset.X), (int)(hitbox.Center.Y  - offset.Y), spritesheet.Width / frameCount, spritesheet.Height), new Rectangle(spritesheet.Width / frameCount * currentFrame, 0, spritesheet.Width / frameCount, spritesheet.Height), Color.White, hitbox.RotationRad, animationOffset, SpriteEffects.None, 0f);

            return true;
     
        }

        public void Reset()
        {
            currentFrame = 0;
        }

        //public void SetPosition(Vector2 position)


    }
}
