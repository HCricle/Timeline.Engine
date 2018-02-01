using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Timeline.Framework.Drawing.Image;
using Timeline.Framework.Drawing.Layers;
using Timeline.Framework.Drawing.SpriteObject;
using Timeline.Framework.SpriteObject.SpriteBase;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Web.Http;

namespace Timeline.Framework.SpriteObject
{
    /// <summary>
    /// 表示一张图片...
    /// </summary>
    public class ImgSprite : StickersSprite
    {
        public ImgSprite(Layer layer) : base(layer)
        {
        }

        protected sealed override void Draw()
        {
            if (Bitmap!=null)
            {
                ds.DrawImage(Bitmap,Bound);
            }
        }
        
    }
}
