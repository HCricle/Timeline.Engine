using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timeline.Framework.Drawing.PaintingSuface;

namespace Timeline.Framework.Drawing.Layers
{
    /// <summary>
    /// 表示一个静态图层
    /// </summary>
    public class StaticLayer : Layer
    {
        public StaticLayer(Page page) : base(page)
        {
        }
        protected sealed override void Draw()
        {
            ds.DrawImage(cl);
        }

        protected sealed override void Update()
        {
            if (cl == null)
            {
                cl = new CanvasCommandList(ResourceCreator);
                using (var ds = cl.CreateDrawingSession())
                {
                    var args = new CanvasAnimatedDrawEventArgs(ds, Time);
                    foreach (var item in SpriteContainer.GetSprites())
                    {
                        item.ObjectUpdate(ResourceCreator, Time);
                        item.ObjectDraw(ResourceCreator, args);
                    }
                }
            }
        }
        /// <summary>
        /// 使得布局无效化，然后进行更新，再重画
        /// </summary>
        public void Invail()
        {
            cl?.Dispose();
        }
    }
}
