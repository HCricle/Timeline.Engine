using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timeline.Framework.Controller;
using Timeline.Framework.Drawing.Layers;

namespace Timeline.Framework.Drawing.SpriteObject
{
    /// <summary>
    /// 表示这个精灵可以被点击，检测鼠标等等
    /// </summary>
    public abstract class ClickableSprite : CanvasControlBase
    {
        public ClickableSprite(Layer layer) 
            : base(layer)
        {
        }
    }
}
