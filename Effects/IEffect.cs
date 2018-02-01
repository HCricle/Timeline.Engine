using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Effects;

namespace Timeline.Framework.Drawing.Effects
{
    public interface IEffect
    {
        void DrawEffect(IGraphicsEffectSource effectSource,CanvasDrawingSession ds);
    }
}
