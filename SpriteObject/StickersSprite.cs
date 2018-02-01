using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timeline.Framework.Drawing.Image;
using Timeline.Framework.Drawing.Layers;
using Timeline.Framework.SpriteObject.SpriteBase;

namespace Timeline.Framework.Drawing.SpriteObject
{
    /// <summary>
    /// 贴图精灵，表示用到一张图的精灵
    /// </summary>
    public abstract class StickersSprite : Sprite
    {
        /// <summary>
        /// 图片键值
        /// </summary>
        protected object imgKey;
        /// <summary>
        /// 图片资源
        /// </summary>
        protected CanvasBitmap bitmap;

        protected StickersSprite(Layer layer) : base(layer)
        {
        }

        public object ImgKey => imgKey;
        public CanvasBitmap Bitmap => bitmap;
        /// <summary>
        /// 设置图片
        /// </summary>
        /// <param name="imgKey">图片键值</param>
        public void SetImg(object imgKey)
        {
            if (imgKey==null)
            {
                return;
            }
            this.imgKey = imgKey;
            bitmap = CanvasImgCacher.TryGetBitmap(imgKey);
            if (bitmap == null)
            {
                throw new ArgumentException($"{imgKey}-没有此键值的图片");
            }
        }
        protected override void FixUpdate()
        {
            if (AutoSize&&Bitmap!=null)
            {
                Height = (float)Bitmap.Bounds.Height;
                Width = (float)Bitmap.Bounds.Width;
            }
            base.Update();
        }
    }
}
