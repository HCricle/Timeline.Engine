using Microsoft.Graphics.Canvas.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timeline.Framework.Controller;
using Timeline.Framework.Drawing.Layers;
using Windows.UI;

namespace Timeline.Framework.Drawing.Controller
{
    /// <summary>
    /// 表示一个一段字,且不会更新的控件
    /// </summary>
    public class CanvasStaticString : CanvasControlBase
    {
        private string text;
        private Color color;
        private CanvasTextLayout layout;
        private CanvasTextFormat format;
        public CanvasStaticString(Layer layer,string text, CanvasTextFormat textFormat=default(CanvasTextFormat),
            Color textColor =default(Color),
            float width= float.PositiveInfinity, float height=float.PositiveInfinity)
            :base(layer)
        {
            this.text = text;
            color = textColor;
            format = textFormat;
            if (format==null)
            {
                format = new CanvasTextFormat();
            }
            Width = width;
            Height = height;
        }
        protected override void Update()
        {
            layout = new CanvasTextLayout(ResourceCreator, text, format, Width, Height);
            base.Update();
        }
        protected sealed override void Draw()
        {
            ds.DrawTextLayout(layout, X, Y, color);
        }
    }
}
