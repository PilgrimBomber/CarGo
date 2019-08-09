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
    public enum AbilityType { Flamethrower, RocketLauncher, Shockwave, TrapLauncher }
    public abstract class ActiveAbility:Entity
    {
        protected float activationCooldownTimer;
        protected float livingTimer;
        public bool isActive;

        public abstract void Use();
       
        public void Cooldown(GameTime gameTime)
        {
            if(livingTimer>0)
            {
                livingTimer -= (float)gameTime.ElapsedGameTime.Milliseconds/1000;
                if (livingTimer < 0)
                {
                    isActive = false;
                    livingTimer = 0;
                }
            }
            
            if(activationCooldownTimer > 0)
            {
                activationCooldownTimer -= (float)gameTime.ElapsedGameTime.Milliseconds / 1000;
                if (activationCooldownTimer < 0) activationCooldownTimer = 0;
            }
        }
    }

}
