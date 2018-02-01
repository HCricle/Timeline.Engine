using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timeline.Framework.Drawing.Deformation
{
    public class ScaleTransform
    {
        public ScaleTransform()
        {
            ScaleX = ScaleY = 1;
        }

        public ScaleTransform(float scaleX, float scaleY)
        {
            ScaleX = scaleX;
            ScaleY = scaleY;
        }

        public float ScaleX { get; set; }
        public float ScaleY { get; set; }
    }
}
