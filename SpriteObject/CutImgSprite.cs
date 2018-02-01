using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timeline.Framework.Drawing.Layers;
using Timeline.Framework.SpriteObject.SpriteBase;
using Windows.Foundation;

namespace Timeline.Framework.Drawing.SpriteObject
{
    /// <summary>
    /// 表示一个可切割显示的图形精灵，RPG游戏经常用
    /// </summary>
    public class CutImgSprite : StickersSprite
    {
        /// <summary>
        /// 切割的大小
        /// </summary>
        private Rect CutBound;

        protected CutImgSprite(Layer layer) : base(layer)
        {
        }

        /// <summary>
        /// 设置切割
        /// </summary>
        /// <param name="cutbound"></param>
        public void SetCutBound(Rect cutbound)
        {
            Debug.Assert(Bitmap != null);
            if (!Bitmap.Bounds.Contains(new Point(cutbound.X + cutbound.Width, cutbound.Y + cutbound.Height))) 
            {
                throw new ArgumentException("设置的图片大小超出了范围");
            }
            CutBound = cutbound;
        }
        public void SetCutBound(float x,float y,float width,float height)
        {
            SetCutBound(new Rect(x, y, width, height));
        }
        protected override void Draw()
        {
            ds.DrawImage(Bitmap, CutBound);
        }
    }
}
