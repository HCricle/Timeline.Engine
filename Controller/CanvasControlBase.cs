using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI;
using Timeline.Framework.Drawing.Layers;
using Timeline.Framework.Input;
using Timeline.Framework.SpriteObject;
using Timeline.Framework.SpriteObject.SpriteBase;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Input;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace Timeline.Framework.Controller
{
    /// <summary>
    /// 画出来的控件,例如文字,图片
    /// </summary>
    public abstract class CanvasControlBase : Sprite,IDisposable
    {        
        private bool isPressed;
        private bool isEnter;

        public CanvasControlBase(Layer layer) : base(layer)
        {
        }

        public bool IsEnter => isEnter;
        public bool IsPressed => isPressed;

        protected virtual void OnPointerMoved()
        {
        }
        protected virtual void OnPointerPressed()
        {
        }
        protected virtual void OnPointerReleased()
        {
        }
        protected virtual void OnPointerEnter()
        {

        }
        protected virtual void OnPointerExited()
        {

        }
        public bool IsPointOn(Point point)
        {
            var rec = Bound;
            return (point.X >= rec.Left && point.X <= rec.Right && 
                point.Y >= rec.Top && point.Y <= rec.Bottom);
        }
        protected override void Update()
        {
            var mi = Mouse.GetInstance();
            if (mi.IsPressed && IsPointOn(mi.PressedPoint))
            {
                isPressed = true;
                OnPointerPressed();
            }
            if (!mi.IsPressed&&IsPressed)
            {
                isPressed = false;
                OnPointerReleased();
            }
            if (IsPointOn(mi.CurrentPoint))
            {
                isEnter = true;
                OnPointerEnter();
            }
            if (IsEnter && !IsPointOn(mi.CurrentPoint))
            {
                isEnter = false;
                OnPointerExited();
            }
            base.Update();
        }
    }
}
