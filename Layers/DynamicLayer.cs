using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timeline.Framework.Drawing.PaintingSuface;
using Timeline.Framework.SpriteObject.SpriteBase;

namespace Timeline.Framework.Drawing.Layers
{
    public class DynamicLayer : Layer
    {
        public DynamicLayer(Page page) : base(page)
        {
        }
        protected sealed override void Draw()
        {
            ds.DrawImage(cl);
        }

        protected sealed override void Update()
        {
            cl = new CanvasCommandList(ResourceCreator);
            using (var ds = cl.CreateDrawingSession())
            {
                var args = new CanvasAnimatedDrawEventArgs(ds, Time);
                var ss = SpriteContainer.GetSprites();
                Sprite s;
                for (int i = 0; i < ss.Count; i++)
                {
                    s = ss.ElementAt(i);
                    s.ObjectUpdate(ResourceCreator, Time);
                    s.ObjectDraw(ResourceCreator, args);
                }
            }
            

        }
    }
}
