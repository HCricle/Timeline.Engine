using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Timeline.Framework.Deformation
{
    public class Transform
    {
        public Transform()
        {
            
        }

        public Transform(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float X { get; set; }
        public float Y { get; set; }
        public void Translate(float x,float y)
        {
            X += x;
            Y += y;
        }
        public static Transform operator +(Transform t1, Transform t2)
        {
            return new Transform(t1.X + t2.X, t1.Y + t2.Y);
        }
        public static Transform operator -(Transform t1, Transform t2)
        {
            return new Transform(t1.X - t2.X, t1.Y - t2.Y);
        }
        public static Transform operator *(Transform t1, Transform t2)
        {
            return new Transform(t1.X * t2.X, t1.Y * t2.Y);
        }
    }
}
