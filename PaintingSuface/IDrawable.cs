using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timeline.Framework.Drawing.PaintingSuface
{
    interface IDrawable
    {
        void Update(ICanvasResourceCreator creator, CanvasTimingInformation time);
        void Draw(ICanvasResourceCreator creator, CanvasAnimatedDrawEventArgs args);
    }
}
