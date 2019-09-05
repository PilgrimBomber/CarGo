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
    public abstract class WorldObject : Entity
    {
        public void SetPosition(Vector2 position)
        {
            hitbox.Move(position-hitbox.Center);
        }
    }
}
