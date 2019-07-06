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
    public static class Geometry
    {

        public static Vector2 Rotate(Vector2 vector, int Degree)
        {
            //float rad = DegToRad(Degree);
            //float s = (float)Math.Sin(rad);
            //float c = (float)Math.Cos(rad);
            //return new Vector2(vector.X * c + vector.Y * s, vector.Y * c - vector.X * s);
            return Rotate(vector, DegToRad(Degree));
        }
        public static Vector2 Rotate(Vector2 vector, float rad)
        {
            float s = (float)Math.Sin(rad);
            float c = (float)Math.Cos(rad);
            return new Vector2(vector.X * c - vector.Y * s, vector.Y * c + vector.X * s);
        }
        public static Vector2 Rotate(Vector2 point, int deg, Vector2 origin)
        {
            return Rotate(point, DegToRad(deg), origin);
        }
        public static Vector2 Rotate(Vector2 point, float rad,Vector2 origin)
        {
            Vector2 vector = new Vector2();
            vector = point - origin;
            float s = (float)Math.Sin(rad);
            float c = (float)Math.Cos(rad);
            float oldX = vector.X;
            float oldY = vector.Y;
            vector.X = oldX * c - oldY * s;
            vector.Y = oldY * c + oldX * s;
            return vector + origin;
        }

        public static int RadToDeg(float rad)
        {
            return (int)(rad / Math.PI * 180);
        }

        public static float DegToRad(int deg)
        {
            return (float)((float)deg / 180 * Math.PI);
        }
    }
}
