using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Timeline.Framework.Drawing.Layers;
using Timeline.Framework.Drawing.Object;
using Timeline.Framework.Drawing.SpriteObject;
using Timeline.Framework.Effects;
using Windows.Foundation;
using Windows.Graphics.Effects;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Timeline.Framework.SpriteObject.SpriteBase
{
    /// <summary>
    /// 表示一个2d静态精灵
    /// </summary>
    public abstract class Sprite : ModifiableObject
    {
        protected Sprite(Layer layer)
        {
            Layer = layer;
            IsActive = true;
            AutoSize = true;
        }
        //改成Iot控制反转
        //就是Sprite有Layer属性
        //Layer有Page属性
        //Page有Book属性
        public Layer Layer { get; }
        /// <summary>
        /// 自动大小，会根据图片，字体等等的大小设置自己大小
        /// </summary>
        public bool AutoSize { get; set; }
        /// <summary>
        /// 页摄像机
        /// </summary>
        public Camera Camera => Layer.Page.Camera;
        public sealed override void ObjectDraw(ICanvasResourceCreator creator, CanvasAnimatedDrawEventArgs args)
        {
            if (IsActive)
            {
                ds = args.DrawingSession;

                Draw();
                FixUpdate();
            }
        }
        public sealed override void ObjectUpdate(ICanvasResourceCreator creator, CanvasTimingInformation args)
        {
            if (IsActive)
            {
                Time = args;
                ResourceCreator = creator;
                Update();//精灵更新
                foreach (var item in GetComponents())//组件更新
                {
                    item.OnUpdate(args);
                }
            }
        }
        /// <summary>
        /// 绘画
        /// </summary>
        protected abstract void Draw();
        /*
        public bool CollidesWith(Sprite rc, bool calPreColor = false)
        {            
            if (IsActive) 
            {
                var res = CalNormalCollised(Bound, rc);
                var rb = rc.Bound;
                var widthOther = rb.Width;
                var heightOther = rb.Height;
                var widthMe = Width;
                var heightMe = Height;
                //下面判断色点碰撞
                if (calPreColor && res &&
                    ((Math.Min(widthOther, heightOther) < 100) ||
                    (Math.Min(widthMe, heightMe) < 100))) 
                {
                    res = CollidesPreColor(ActualBound,PreColor,PixelWidth,PixelHeight,
                        rc.ActualBound, rc.PreColor, rc.PixelWidth, rc.PixelHeight);
                }
                return res;
            }
            return false;
        }
        protected static bool CalNormalCollised(Rect rect,Sprite rc)
        {
            var sbound = rc.Bound;
            var ibound = rect;


            var widthOther = sbound.Width;
            var heightOther = sbound.Height;
            var widthMe = ibound.Width;
            var heightMe = ibound.Height;

            ibound.Intersect(rc.Bound);
            if (ibound.IsEmpty)
            {
                sbound.Intersect(rect);
                ibound = sbound;
            }
            return ibound != Rect.Empty;
        }
        protected static bool CollidesPreColor(Rect rc1, Color[] colors1,int pxw1, int pxh1,
            Rect rc2, Color[] colors2, int pxw2, int pxh2)
        {
            if (colors1 == null || colors2 == null
                || colors1.Count() == 0
                || colors2.Count() == 0
                || rc1 == Rect.Empty
                || rc2 == Rect.Empty) 
            {
                return false;
            }
            var ibound = rc1;
            var sbound = rc2;  
            
            var x1 = Math.Max(ibound.X, sbound.X);
            var x2 = Math.Min(ibound.X + pxw1, sbound.X + pxw2);

            var y1 = Math.Max(ibound.Y, sbound.Y);
            var y2 = Math.Min(ibound.Y + pxh1, sbound.Y + pxh2);//这里第二个不知是pxw1还是pxh1
            Windows.UI.Color ca,cb;
            int la, lb;
            for (int y = (int)y1; y < y2; ++y)
            {
                for (int x = (int)x1; x < x2; ++x)
                {
                    la = (int)((x - ibound.X) + (y - ibound.Y) * pxw1);
                    lb = (int)((x - sbound.X) + (y - sbound.Y) * pxw2);
                    if (la<colors1.Count()&&lb<colors2.Count())
                    {
                        ca = colors1[Math.Abs(la)];
                        cb = colors2[Math.Abs(lb)];
                        if (ca.A == 0 && cb.A == 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
            
        }
        */
    }
}
