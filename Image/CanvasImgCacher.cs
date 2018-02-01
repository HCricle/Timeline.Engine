using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Microsoft.Graphics.Canvas;
using Windows.Storage.Streams;
using System.Diagnostics;
using Windows.UI;

namespace Timeline.Framework.Drawing.Image
{
    public static class CanvasImgCacher
    {
        static CanvasImgCacher()
        {
            Bitmaps = new ConcurrentDictionary<object, CanvasBitmap>();
            StaticBitmaps = new ConcurrentDictionary<object, CanvasBitmap>();
        }
        private static ConcurrentDictionary<object,CanvasBitmap> Bitmaps { get; }
        /// <summary>
        /// 这个是不会被销毁的图片
        /// </summary>
        private static ConcurrentDictionary<object, CanvasBitmap> StaticBitmaps { get; }
        /// <summary>
        /// 从流中缓存图片
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="creator">资源创造器</param>
        /// <param name="stream">图片流</param>
        /// <returns></returns>
        public static async Task<bool> CacheBitmap(object key,ICanvasResourceCreator creator,IRandomAccessStream stream)
        {
            return await CacheBitmap(key, creator, async() => await CanvasBitmap.LoadAsync(creator, stream));
        }
        /// <summary>
        /// 从流中缓存静态图片
        /// </summary>
        /// <param name="key"></param>
        /// <param name="creator"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static async Task<bool> StaticCacheBitmap(object key, ICanvasResourceCreator creator, IRandomAccessStream stream)
        {
            return await StaticCacheBitmap(key, creator, async () => await CanvasBitmap.LoadAsync(creator, stream));
        }
        /// <summary>
        /// 尝试去获取静态图片
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static CanvasBitmap TryGetStaticBitmap(object key)
        {
            if (key == null)
            {
                throw new ArgumentException("键值不能为空");
            }
            StaticBitmaps.TryGetValue(key, out var bitmap);
            return bitmap;
        }
        /// <summary>
        /// 尝试获取图
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns></returns>
        public static CanvasBitmap TryGetBitmap(object key)
        {
            if (key==null)
            {
                throw new ArgumentException("键值不能为空");
            }
            Bitmaps.TryGetValue(key, out var bitmap);
            return bitmap;
        }
        /// <summary>
        /// 获取所有图
        /// </summary>
        /// <returns></returns>
        public static CanvasBitmap[] GetAllBitmap()
        {
            return Bitmaps.Values.ToArray();
        }
        private static async Task<bool> StaticCacheBitmap(object key, ICanvasResourceCreator creator, Func<Task<CanvasBitmap>> func)
        {
            if (TryGetStaticBitmap(key) != null)
            {
                throw new ArgumentException($"{key}-此键值已经有数据了！！！");
            }
            var bitmap = await func();
            Debug.Assert(bitmap != null);
            return StaticBitmaps.TryAdd(key, bitmap);
        }

        /// <summary>
        /// 自己用的
        /// </summary>
        /// <param name="key"></param>
        /// <param name="creator"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        private static async Task<bool> CacheBitmap(object key,ICanvasResourceCreator creator,Func<Task<CanvasBitmap>> func)
        {            
            if (TryGetBitmap(key) != null)
            {
                throw new ArgumentException($"{key}-此键值已经有数据了！！！");
            }
            var bitmap = await func.Invoke();
            Debug.Assert(bitmap != null);
            return Bitmaps.TryAdd(key, bitmap);
        }
        /// <summary>
        /// 从uri加载静态图片
        /// </summary>
        /// <param name="key"></param>
        /// <param name="creator"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static async Task<bool> StaticCacheBitmap(object key, ICanvasResourceCreator creator, Uri uri)
        {
            return await StaticCacheBitmap(key, creator, async () => await CanvasBitmap.LoadAsync(creator, uri));
        }
        /// <summary>
        /// 从uri加载图片
        /// </summary>
        /// <param name="key"></param>
        /// <param name="creator"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static async Task<bool> CacheBitmap(object key, ICanvasResourceCreator creator,Uri uri)
        {
            return await CacheBitmap(key, creator, async () => await CanvasBitmap.LoadAsync(creator, uri));
        }
        /// <summary>
        /// 从路径加载图片
        /// </summary>
        /// <param name="key"></param>
        /// <param name="creator"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static async Task<bool> StaticCacheBitmap(object key, ICanvasResourceCreator creator, string path)
        {
            return await StaticCacheBitmap(key, creator, async () => await CanvasBitmap.LoadAsync(creator, path));
        }
        /// <summary>
        /// 从路径加载图片
        /// </summary>
        /// <param name="key"></param>
        /// <param name="creator"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static async Task<bool> CacheBitmap(object key, ICanvasResourceCreator creator, string path)
        {
            return await CacheBitmap(key, creator, async () => await CanvasBitmap.LoadAsync(creator, path));
        }
        /// <summary>
        /// 从色点加载静态图片
        /// </summary>
        /// <param name="key"></param>
        /// <param name="creator"></param>
        /// <param name="colors"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static bool StaticCacheBitmap(object key, ICanvasResourceCreator creator, Color[] colors, int width, int height)
        {
            if (TryGetStaticBitmap(key) != null)
            {
                throw new ArgumentException($"{key}-此键值已经有数据了！！！");
            }
            Debug.Assert(colors != null);
            var bitmap = CanvasBitmap.CreateFromColors(creator, colors, width, height);
            return StaticBitmaps.TryAdd(key, bitmap);
        }
        /// <summary>
        /// 从颜色点加载图片
        /// </summary>
        /// <param name="key"></param>
        /// <param name="creator"></param>
        /// <param name="colors"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static bool CacheBitmap(object key, ICanvasResourceCreator creator,Color[] colors,int width,int height)
        {
            if (TryGetBitmap(key) != null)
            {
                throw new ArgumentException($"{key}-此键值已经有数据了！！！");
            }
            Debug.Assert(colors != null);
            var bitmap = CanvasBitmap.CreateFromColors(creator, colors, width, height);
            return Bitmaps.TryAdd(key, bitmap);
        }
        /// <summary>
        /// 尝试移除图片并释放，建议用，图片资源会被释放
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool TryDisposeBitmap(object key)
        {
            var res = Bitmaps.TryRemove(key, out var bitmap);
            bitmap?.Dispose();
            return res;
        }
        /// <summary>
        /// 尝试移除静态图片并释放，建议用，图片资源会被释放
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool TryDisposeStaticBitmap(object key)
        {
            var res = StaticBitmaps.TryRemove(key, out var bitmap);
            bitmap?.Dispose();
            return res;
        }
        /// <summary>
        /// 尝试移除图片，不建议用，因为图片资源没被释放
        /// </summary>
        /// <returns></returns>
        public static bool TryRemoveBitmap(object key)
        {
            return Bitmaps.TryRemove(key, out var bitmap);
        }
        /// <summary>
        /// 获取所有静态图片
        /// </summary>
        /// <returns></returns>
        public static IReadOnlyCollection<CanvasBitmap> GetStaticBitmaps()
            => StaticBitmaps.Values.ToArray();
        /// <summary>
        /// 尝试移除静态图片,..
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool TryRemoveStaticBitmap(object key)
        {
            return StaticBitmaps.TryRemove(key, out var bitmap);
        }
        /// <summary>
        /// 清除并释放所有图片资源
        /// </summary>
        public static void Dispose()
        {
            lock (Bitmaps)
            {
                foreach (var item in Bitmaps)
                {
                    item.Value.Dispose();
                }
                Bitmaps.Clear();
            }
        }
    }
}
