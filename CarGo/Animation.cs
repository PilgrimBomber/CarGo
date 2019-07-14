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
    public enum AnimationType {Explosion}

    class Animation
    {
        private float totalTimeExisting;
        private float timeExisting;
        private AnimationType animationType;
        public Animation(float duration,AnimationType animationType)
        {
            totalTimeExisting = duration;
            timeExisting = 0;
            this.animationType = animationType;

        }

        public void Draw(GameTime gameTime)
        {

        }
    }
}
