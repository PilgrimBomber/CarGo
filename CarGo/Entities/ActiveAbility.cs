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
    public enum AbilityType { RocketLauncher}
    public abstract class ActiveAbility
    {
        public abstract void Use();

    }


    public class RocketLauncher:ActiveAbility
    {
        public RocketLauncher(Player player)
        {

        }
        public override void Use()
        {
            throw new NotImplementedException();
        }

        
    }
}
