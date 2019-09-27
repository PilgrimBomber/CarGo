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
        private float rotationRad;
        private Vector2 offset;
        private Vector2 center;
        private Vector2[] corners;
        
        //Create a Rectangle with center mid and an offset. the offset equals half the width and length of the hitbox
        public RotRectangle(float rotationRad, Vector2 center, Vector2 offset)
        {
            this.rotationRad = rotationRad;
            this.offset = offset;
            this.center = center;
            corners = new Vector2[4];
            corners[0] = this.center + offset;
            corners[1] = this.center + new Vector2(offset.X, -offset.Y);
            corners[2] = this.center - offset;
            corners[3] = this.center + new Vector2(-offset.X, offset.Y);
            for (int i = 0; i < 4; i++)
            {
                corners[i] = Geometry.Rotate(corners[i], rotationRad, this.center);
            }
        }

        public float RotationRad
        {
            get { return rotationRad; }
            set { rotationRad = value; }
        }

        public Vector2[] Corners { get => corners;}
        public Vector2 Center { get => center;}
        public Vector2 Offset { get => offset;}

        public void SetPosition(Vector2 position)
        {
            Move(position - center);
            
        }

        public void Move(Vector2 direction)
        {
            center.X += direction.X;
            center.Y += direction.Y;
            for (int i = 0; i < 4; i++)
            {
                corners[i] += direction;
            }
        }

        public void Rotate(float rad)
        {
            rotationRad += rad;
            for (int i = 0; i < 4; i++)
            {
                corners[i] = Geometry.Rotate(corners[i], rad, center);
            }

        }

        public void RotatePoint(float rad, Vector2 origin)
        {
            rotationRad += rad;
            for (int i = 0; i < 4; i++)
            {
                corners[i] = Geometry.Rotate(corners[i], rad, origin);
            }
            center= Geometry.Rotate(center, rad, origin);
        }
        

    }
}
