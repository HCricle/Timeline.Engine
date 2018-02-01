using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timeline.Framework.SpriteObject.SpriteBase;

namespace Timeline.Framework.Drawing.Layers
{
    /// <summary>
    /// 精灵容器
    /// </summary>
    public class SpriteContainter
    {
        /// <summary>
        /// 精灵集合
        /// </summary>
        private List<Sprite> Sprites;

        public SpriteContainter()
        {
            Sprites = new List<Sprite>();
        }
        /// <summary>
        /// 生成一个精灵
        /// </summary>
        /// <typeparam name="TSprite"></typeparam>
        /// <param name="isStatic"></param>
        /// <returns></returns>
        public TSprite BuildSprite<TSprite>(Layer layer)
            where TSprite : Sprite
        {
            lock (Sprites)
            {

                var sprite = (TSprite)Activator.CreateInstance(typeof(TSprite), layer);
                Sprites.Add(sprite);
                return sprite;
            }
        }
        /// <summary>
        /// 加入一个精灵
        /// </summary>
        /// <param name="sprite"></param>
        public void InsertSprite(Sprite sprite)
        {
            Sprites.Add(sprite);
        }
        /// <summary>
        /// 在绘画列表中去除一个精灵,不建议
        /// </summary>
        /// <param name="sprite"></param>
        /// <returns></returns>
        public void RemoveSprite(Sprite sprite)
        {
            lock (Sprites)
            {

                var sp = Sprites.Where(s => s == sprite);
                Debug.Assert(sp != null);
                Sprites.Remove(sp.ElementAt(0));
            }
        }
        /// <summary>
        /// 销毁一个精灵
        /// </summary>
        /// <param name="sprite"></param>
        public void DestorySprite(Sprite sprite)
        {
            lock (Sprites)
            {

                RemoveSprite(sprite);
            }
        }
        /// <summary>
        /// 清除绘画列表的所有精灵,不建议
        /// </summary>
        public void CleanSprite()
        {
            lock (Sprites)
            {

                Sprites.Clear();
            }
        }
        /// <summary>
        /// 销毁所有精灵
        /// </summary>
        public void DestorySprites()
        {
            lock (Sprites)
            {
                foreach (var item in Sprites)
                {
                    item.Dispose();
                }
                Sprites.Clear();
            }
        }
        public IReadOnlyCollection<Sprite> GetSprites()
        {
            return Sprites;
        }
    }
}
