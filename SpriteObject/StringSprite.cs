using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Timeline.Framework.Drawing.Layers;
using Timeline.Framework.SpriteObject.SpriteBase;

namespace Timeline.Framework.SpriteObject
{
    /// <summary>
    /// 表示一段文本图形
    /// </summary>
    public class StringSprite : GeometrySprite
    {
        protected CanvasTextLayout TextLayout;
        public string Text { get; set; }
        public CanvasTextFormat TextFormat { get; set; }

        public StringSprite(Layer layer) : base(layer)
        {
            Text = "";
            TextFormat = new CanvasTextFormat();
            Width = Height = 100f;
        }

        protected override void Update()
        {
            TextLayout?.Dispose();
            if (AutoSize)
            {
                TextLayout = new CanvasTextLayout(ResourceCreator, Text, TextFormat, float.PositiveInfinity, float.PositiveInfinity);
                Height = (float)TextLayout.DrawBounds.Height;
                Width = (float)TextLayout.DrawBounds.Width;
            }
            else
            {
                TextLayout = new CanvasTextLayout(ResourceCreator, Text, TextFormat, Width, Height);
            }
            geometry = CanvasGeometry.CreateText(TextLayout);

            base.Update();
        }

    }
}
