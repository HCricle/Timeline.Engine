using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timeline.Framework.Drawing.Layers;
using Windows.UI;
using Windows.UI.Xaml;

namespace Timeline.Framework.Controller
{
    public abstract class PaintCanvasControlBase : CanvasControlBase
    {
        public Color BackgroundColor { get; set; }
        public Color BorderColor { get; set; }
        public float StrokeWidth { get; set; }
        protected ICanvasBrush backgroundBrush,borderBrush;
        public bool HasBrush => backgroundBrush != null;//随便一个

        public PaintCanvasControlBase(Layer layer) : base(layer)
        {
            BackgroundColor = BorderColor = Colors.Transparent;
            StrokeWidth = 1f;
        }

        protected void UpdateBrush(ICanvasResourceCreator creator)
        {
            backgroundBrush?.Dispose();
            backgroundBrush = new CanvasSolidColorBrush(creator, BackgroundColor);
            borderBrush?.Dispose();
            borderBrush = new CanvasSolidColorBrush(creator, BorderColor);
        }
        protected override void Draw()
        {
            if (!HasBrush)
            {
                UpdateBrush(ResourceCreator);
            }
            ds.FillRectangle(Bound, backgroundBrush);
            ds.DrawRectangle(Bound, borderBrush, StrokeWidth);
        }
    }
}
