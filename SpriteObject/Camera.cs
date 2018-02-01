using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timeline.Framework.Drawing.Layers;
using Timeline.Framework.Drawing.Object;
using Timeline.Framework.Input;
using Timeline.Framework.SpriteObject.SpriteBase;
using Windows.Foundation;
using Windows.UI.Core;

namespace Timeline.Framework.Drawing.SpriteObject
{
    /// <summary>
    /// 摄像机
    /// 
    /// 摄像机在Page内
    /// 
    /// 最终图形是由摄像机裁剪的
    /// </summary>
    public sealed class Camera : VisibleObject
    {
        public Camera()
        {            
            Width = (float)CurrentWindow.Window.Bounds.Width;
            Height = (float)CurrentWindow.Window.Bounds.Height ;//默认看到的是300x300
            Opacity = 1f;
            ImageInterpolation = default(CanvasImageInterpolation);
            Composite = default(CanvasComposite);
        }
        private float opacity;

        public float Opacity
        {
            get => opacity;
            set
            {
                if (value>1||value<0)
                {
                    throw new ArgumentException("透明的错误");
                }
                opacity = value;
            }
        }
        /// <summary>
        /// 摄像机质量
        /// </summary>
        public CanvasImageInterpolation ImageInterpolation { get; set; }
        /// <summary>
        /// ????
        /// </summary>
        public CanvasComposite Composite { get; set; }
        /// <summary>
        /// 宽度倍数
        /// </summary>
        public float WidthMul { get; set; }
        /// <summary>
        /// 高度倍数
        /// </summary>
        public float HeightMul { get; set; }
        /// <summary>
        /// 裁剪图形
        /// </summary>
        public void Crup(ICanvasResourceCreator creator,ICanvasImage source,CanvasDrawingSession ds)
        {
            ds.DrawImage(source, new Rect(0, 0, Width, Height), Bound, Opacity, ImageInterpolation, Composite);//TODO
        }
        /// <summary>
        /// 设置视野
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetVision(float width,float height)
        {
            if (width<0||height<0)
            {
                throw new ArgumentException("设置视野参数错误");
            }
            Width = width;
            Height = height;
        }
        private Rect rc;
        private void  UpdateRect()
        {
            rc = CurrentWindow.Window.Bounds;
        }
        /// <summary>
        /// 让摄像机看着这个物体
        /// </summary>
        /// <param name="vo">看向的物体</param>
        public void LookAt(VisibleObject vo)
        {
            if (CoreWindow.GetForCurrentThread()==null)
            {
                CurrentWindow.RunInWindow(UpdateRect);
            }
            else
            {
                rc = CoreWindow.GetForCurrentThread().Bounds;
            }
            X = vo.X + vo.Width / 2f-Width/2f;
            Y = vo.Y + vo.Height / 2f - Height / 2f;
            if (X<0)
            {
                X = 0;
            }
            else if(X>rc.Width)
            {
                X = (float)rc.Width;
            }

            if (Y < 0)
            {
                Y = 0;
            }
            else if (Y > rc.Height)
            {
                Y = (float)rc.Height;
            }
        }
    }
}
