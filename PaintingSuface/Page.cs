using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using Timeline.Framework.Drawing.Layers;
using Timeline.Framework.Drawing.SpriteObject;
using Timeline.Framework.Input;
using Timeline.Framework.SpriteObject;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace Timeline.Framework.Drawing.PaintingSuface
{
    /*
     * 创造步骤
     * 1,Page(页),一个时刻只能有一个页
     * 2,创建图层(Layer),一个页可以有多个图层
     * 3,创建Sprite(精灵),一个图层可以有多个精灵
     * 
     * 用户需要
     * 一个物体继承于(不继承也可以)某一个Sprite(可以是原始的,也可以是其它的),编写自己的精灵
     * 继承Layer(StaticLayer与DynamicLayer选一个)
     * 在图层中加入精灵,事件以及组件等等
     * Page,ok
     * 
     * 一个Suface包含若干个图层,Suface执行派发更新,绘画等等(未定)
     */

    /// <summary>
    /// 表示只有一页,抽象于一本书的一页(单页)
    /// </summary>
    public abstract class Page:IPage,IDrawable
    {
        private List<Layer> Layers;
        /// <summary>
        /// 是否已经创建了资源
        /// </summary>
        private bool isInit;
        private CanvasCommandList cl;
        private Camera camera;
        //public Camera Camera { get; }
        public Page(Book book)
        {
            Book = book;
            Layers = new List<Layer>();
            camera = new Camera();
            //Camera = new Camera();
            //Camera.SetVision(CurrentWindow.WindowWidth, CurrentWindow.WindowHeight);
        }
        public Book Book { get; }
        /// <summary>
        /// 摄像机
        /// </summary>
        public Camera Camera => camera;
        public TLayer BuildLayer<TLayer>()
            where TLayer:Layer
        {
            var tl = (TLayer)Activator.CreateInstance(typeof(TLayer), this);
            Layers.Add(tl);
            return tl;
        }
        public void AddLayer(Layer layer)
        {
            Layers.Add(layer);
        }
        public void DestoryLayer(Layer layer)
        {
            Layers.Remove(layer);
            layer.Dispose();
        }
        public void DestoryLayer(string name)
        {
            var ls = Layers.Where(l => l.Name == name);
            if (ls != null)
            {
                DestoryLayer(ls);
            }
        }
        public void DestoryLayer(IEnumerable<Layer> layers)
        {
            for (int i = 0; i < layers.Count(); i++)
            {
                DestoryLayer(layers.ElementAt(i));
            }
            
        }
        public void DestoryLayer<TLayer>()
            where TLayer : Layer
        {
            var ls = Layers.Where(l => l is TLayer);
            if (ls!=null)
            {
                DestoryLayer(ls);
            }
        }
        /// <summary>
        /// 销毁所有图层
        /// </summary>
        public void DestoryAllLayer()
        {
            DestoryLayer(Layers);
        }
        public IReadOnlyCollection<Layer> GetLayers()
            => Layers;
        /// <summary>
        /// 创建资源,只有一次
        /// </summary>
        /// <param name="creator"></param>
        protected virtual void CreateResource(ICanvasResourceCreator creator) { }
        /// <summary>
        /// 页更新
        /// </summary>
        /// <param name="creator"></param>
        /// <param name="time"></param>
        public void Update(ICanvasResourceCreator creator, CanvasTimingInformation time)
        {
                cl = new CanvasCommandList(creator);
                if (!isInit)
                {
                    isInit = true;
                    CreateResource(creator);
                }
                for (int i = 0; i < Layers.Count; i++)
                {
                    Layers[i].LayerUpdate(creator, time);
                }
        }
        /// <summary>
        /// 页绘画
        /// </summary>
        /// <param name="creator"></param>
        /// <param name="args"></param>
        public void Draw(ICanvasResourceCreator creator, CanvasAnimatedDrawEventArgs args)
        {
            if (cl != null)
            {
                using (var ds = cl.CreateDrawingSession())
                {
                    var e = new CanvasAnimatedDrawEventArgs(ds, args.Timing);
                    foreach (var item in Layers)
                    {
                        item.LayerDraw(creator, e);
                        //item.DrawEffect(args.DrawingSession);
                    }
                }

                Camera.Crup(creator, cl, args.DrawingSession);
                cl.Dispose();
            }
        }

        public void Dispose()
        {
            DestoryAllLayer();
        }
    }
}
