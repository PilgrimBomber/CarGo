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
    public class RotRectangle
    {
        public int rotationDeg;
        public float rotationRad;
        public Rectangle rectangle;
        public Vector2 offset;
        public Vector2 middle;
        public RotRectangle(int rot, Rectangle rec)
        {
            rotationDeg = rot;
            rotationRad = (float)((float)rot / 180 * Math.PI);
            rectangle = rec;
            offset = new Vector2(rectangle.Width / 2, rectangle.Height / 2);
            middle = new Vector2(rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height / 2);
            //Middle = new Vector2(0, 0);
        }

    }
}
