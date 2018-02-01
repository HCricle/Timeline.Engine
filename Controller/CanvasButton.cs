using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Timeline.Framework.Drawing.Layers;
using Timeline.Framework.Input;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace Timeline.Framework.Controller
{
    public class CanvasButton : PaintCanvasControlBase
    {
        public CanvasButton(Layer layer) : base(layer)
        {
            Text = string.Empty;
            Width = Height = 100;
            TextFormat = new CanvasTextFormat();
            TextColor = Colors.Black;
        }

        public string Text { get; set; }
        public Color TextColor { get; set; }
        public CanvasTextFormat TextFormat { get; }
        private CanvasTextLayout layout;
        private void MeshSize()
        {
            layout?.Dispose();
            layout = new CanvasTextLayout(ResourceCreator, Text, TextFormat, Width, Height);
        }
        protected override void Update()
        {
            MeshSize();
            base.Update();
        }
        protected override void Draw()
        {
            base.Draw();
            var b = Bound;
            ds.DrawTextLayout(layout, (float)b.X, (float)b.Y, TextColor);
        }
    }
}
