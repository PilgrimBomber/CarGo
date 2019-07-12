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
    public class CollisionCheck
    {
        public CollisionCheck()
        {

        }
        public void CheckCollisons(List<Entity> entities)
        {
            for (int i = 0; i <entities.Count-1; i++)
            {
                for (int j = i+1; j < entities.Count; j++)
                {
                    if(CheckCollision(entities[i].Hitbox,entities[j].Hitbox))
                    {
                        entities[i].Collide(entities[j]);
                        entities[j].Collide(entities[i]);
                    }
                }
            }
        }

        private bool CheckCollision(RotRectangle r1,RotRectangle r2)
        {
            //Check if they are not in range of each other
            if(r1.Offset.Length()+r2.Offset.Length()<(r1.Center-r2.Center).Length())
                return false;
            else
            {
                //If they are in range of each other
                //Use the seperating axis theorem (SAT) to check if they collide
                Vector2 r1Axis1 = r1.Corners[3] - r1.Corners[0];
                Vector2 r1Axis2 = r1.Corners[1] - r1.Corners[0];
                Vector2 r2Axis1 = r2.Corners[3] - r2.Corners[0];
                Vector2 r2Axis2 = r2.Corners[1] - r2.Corners[0];

                //for each axis
                //project the Corners of both rectangles on the axis and check if there is a gap between them
                //if there is a gap then there is no colission
                List<float> valuesR1=new List<float>();
                List<float> valuesR2 = new List<float>();
                //r1Axis1
                for (int i = 0; i < 4; i++)
                {
                    valuesR1.Add(Vector2.Dot(r1.Corners[i], r1Axis1) / r1Axis1.LengthSquared());
                }
                for (int i = 0; i < 4; i++)
                {
                    valuesR2.Add(Vector2.Dot(r2.Corners[i], r1Axis1) / r1Axis1.LengthSquared());
                }
                if (valuesR1.Min() > valuesR2.Max() || valuesR1.Max() < valuesR2.Min())
                    return false;
                //r1Axis2
                valuesR1.Clear();
                valuesR2.Clear();
                for (int i = 0; i < 4; i++)
                {
                    valuesR1.Add(Vector2.Dot(r1.Corners[i], r1Axis2) / r1Axis2.LengthSquared());
                }
                for (int i = 0; i < 4; i++)
                {
                    valuesR2.Add(Vector2.Dot(r2.Corners[i], r1Axis2) / r1Axis2.LengthSquared());
                }
                if (valuesR1.Min() > valuesR2.Max() || valuesR1.Max() < valuesR2.Min())
                    return false;

                //r2Axis1
                valuesR1.Clear();
                valuesR2.Clear();
                for (int i = 0; i < 4; i++)
                {
                    valuesR1.Add(Vector2.Dot(r1.Corners[i], r2Axis1) / r2Axis1.LengthSquared());
                }
                for (int i = 0; i < 4; i++)
                {
                    valuesR2.Add(Vector2.Dot(r2.Corners[i], r2Axis1) / r2Axis1.LengthSquared());
                }
                if (valuesR1.Min() > valuesR2.Max() || valuesR1.Max() < valuesR2.Min())
                    return false;

                //r2Axis2
                valuesR1.Clear();
                valuesR2.Clear();
                for (int i = 0; i < 4; i++)
                {
                    valuesR1.Add(Vector2.Dot(r1.Corners[i], r2Axis2) / r2Axis2.LengthSquared());
                }
                for (int i = 0; i < 4; i++)
                {
                    valuesR2.Add(Vector2.Dot(r2.Corners[i], r2Axis2) / r2Axis2.LengthSquared());
                }
                if (valuesR1.Min() > valuesR2.Max() || valuesR1.Max() < valuesR2.Min())
                    return false;


                //if there is no gap on any axe the rectangles collide
                return true;
            }
        }
        
        
    }
}
