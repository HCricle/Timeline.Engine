using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace Timeline.Framework.Drawing.Deformation
{
    public static class RectExtend
    {
        public static Rect Copy(this Rect rc)
        {
            return new Rect(rc.X, rc.Y, rc.Width, rc.Height);
        }
        
    }
}
