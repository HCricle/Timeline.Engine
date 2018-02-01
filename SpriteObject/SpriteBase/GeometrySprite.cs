using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Timeline.Framework.Drawing.Layers;
using Windows.UI;

namespace Timeline.Framework.SpriteObject.SpriteBase
{
    /// <summary>
    /// 几何图形，除了图片，其它全部都要继承于这个
    /// </summary>
    public abstract class GeometrySprite : Sprite
    {

        public GeometrySprite(Layer layer) 
            : base(layer)
        {
            IsFill = needUpdateBrush = false;
            StrokeWidth = 1f;
            StrokeStyle = new CanvasStrokeStyle();
            SetBrushColor(Colors.Black);
        }

        private CanvasBrushTypes brushsType;
        private bool needUpdateBrush;
        private Color canvasColor;
        private ICanvasBrush brush;//画刷
        private CanvasGradientStop[] canvasRadialGradientStops;
        private CanvasGradientStop[] canvasLinearGradientBrush;

        protected CanvasGeometry geometry;//图形

        public bool IsFill { get; set; }//是否填充
        public float StrokeWidth { get; set; }//笔宽
        public CanvasStrokeStyle StrokeStyle { get; set; }
        public void SetBrushColor(Color color)
        {
            brushsType = CanvasBrushTypes.SolidColorBrush;
            canvasColor = color;
            needUpdateBrush = true;
        }
        public void SetBrushGradient(CanvasGradientStop[] gradientStops)
        {
            brushsType = CanvasBrushTypes.RadialGradientBrush; ;
            canvasRadialGradientStops = gradientStops;
            needUpdateBrush = true;
        }
        public void SetBrushLinear(CanvasGradientStop[] gradientStops)
        {
            brushsType = CanvasBrushTypes.LinearGradientBrush; ;
            canvasLinearGradientBrush = gradientStops;
            needUpdateBrush = true;
        }
        /// <summary>
        /// 更新，更新包含画刷,要重写，因为几何图形需要更新
        /// </summary>
        protected override void Update()
        {
            if (needUpdateBrush)
            {
                needUpdateBrush = false;
                brush?.Dispose();
                switch (brushsType)
                {
                    case CanvasBrushTypes.SolidColorBrush:
                        brush = new CanvasSolidColorBrush(ResourceCreator, canvasColor);
                        break;
                    case CanvasBrushTypes.RadialGradientBrush:
                        Debug.Assert(canvasRadialGradientStops != null && canvasRadialGradientStops.Count() != 0);
                        brush = new CanvasRadialGradientBrush(ResourceCreator, canvasRadialGradientStops);
                        break;
                    case CanvasBrushTypes.LinearGradientBrush:
                        Debug.Assert(canvasLinearGradientBrush != null && canvasLinearGradientBrush.Count() != 0);
                        brush = new CanvasLinearGradientBrush(ResourceCreator, canvasLinearGradientBrush);
                        break;
                    default:
                        throw new ArgumentException($"错误画刷-{brushsType}");
                }
            }
            
        }
        /// <summary>
        /// 不能被重写，画出来的都是几何图形，包括文字
        /// </summary>
        protected sealed override void Draw()
        {
            Debug.Assert(geometry != null);
            if (IsFill)
                ds.FillGeometry(geometry, X,Y, brush);
            else
                ds.DrawGeometry(geometry, X,Y, brush, StrokeWidth, StrokeStyle);
        }
    }
}
