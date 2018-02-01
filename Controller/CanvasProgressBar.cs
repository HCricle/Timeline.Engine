using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Timeline.Framework.Drawing.Layers;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;

namespace Timeline.Framework.Controller
{
    public class CanvasProgressBar : PaintCanvasControlBase
    {
        public CanvasProgressBar(Layer layer) : base(layer)
        {
        }

        /// <summary>
        /// 进度条当前进度
        /// </summary>
        private float pvalue;

        public float Value
        {
            get { return pvalue; }
            set
            {
                if (value > 1 || value < 0)
                {
                    throw new ArgumentException("进度只能是[0-1]");
                }
                pvalue = value;
            }
        }
        protected override void Draw()
        {
            base.Draw();
            var rcv = new Rect(X, Y, Width * Value, Height);
            ds.DrawRectangle(rcv, BorderColor, StrokeWidth);
        }
    }
}
