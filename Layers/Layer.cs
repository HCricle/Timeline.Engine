using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timeline.Framework.Drawing.Effects;
using Timeline.Framework.Drawing.PaintingSuface;
using Timeline.Framework.Effects;
using Timeline.Framework.SpriteObject.SpriteBase;

namespace Timeline.Framework.Drawing.Layers
{
    /// <summary>
    /// 表示图层,图层内可以有多个精灵,并且图层只有动态和静态图层
    /// </summary>
    public abstract class Layer: ILayer
    {
        /// <summary>
        /// 图层名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 精灵容器
        /// </summary>
        public SpriteContainter SpriteContainer { get; }
        /// <summary>
        /// 特效容器
        /// </summary>
        public IEffectContrainter EffectContrainter { get; }
        /// <summary>
        /// 图层是否活动
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// 给你用的图形后台渲染
        /// </summary>
        protected CanvasCommandList cl;
        /// <summary>
        /// 资源创造器
        /// </summary>
        protected ICanvasResourceCreator ResourceCreator;
        /// <summary>
        /// 帧时间信息
        /// </summary>
        protected CanvasTimingInformation Time;
        /// <summary>
        /// 绘画节
        /// </summary>
        protected CanvasDrawingSession ds;
        public Layer(Page page)
        {
            Page = page;
            IsActive = true;
            SpriteContainer = new SpriteContainter();
            EffectContrainter = new EffectContrainter();
        }
        private bool isInit;
        public Page Page { get; }
        public TSprite BuildSprite<TSprite>()
            where TSprite:Sprite
        {
            return SpriteContainer.BuildSprite<TSprite>(this);
        }
        /// <summary>
        /// 添加特效，存在性能问题
        /// </summary>
        /// <typeparam name="TEffect">特效类型</typeparam>
        /// <returns></returns>
        public CanvasEffect<TEffect> BuildEffect<TEffect>()
            where TEffect: ICanvasImage, new()
        {
            var eff = new CanvasEffect<TEffect>();
            EffectContrainter.AddEffect(eff);
            return eff; 
        }
        public void LayerUpdate(ICanvasResourceCreator creator,CanvasTimingInformation time)
        {
            if (!isInit)
            {
                isInit = true;
                StartUp(creator);
            }
            if (IsActive)
            {
                Time = time;
                ResourceCreator = creator;
                Update();
            }
        }
        public void LayerDraw(ICanvasResourceCreator creator,CanvasAnimatedDrawEventArgs args)
        {
            if (IsActive)
            {              
                ds = args.DrawingSession;
                Draw();
                DrawEffect(ds);
                FixUpdate();                    
            }
        }
        /// <summary>
        /// 绘画特效
        /// </summary>
        private void DrawEffect(CanvasDrawingSession ds)
        {
            foreach (var item in EffectContrainter.GetEffects())
            {
                item.DrawEffect(cl, ds);
            }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void StartUp(ICanvasResourceCreator creator) { }
        /// <summary>
        /// 图层更新
        /// </summary>
        protected abstract void Update();
        /// <summary>
        /// 图形绘画
        /// </summary>
        protected abstract void Draw();
        /// <summary>
        /// 图层正在销毁的时候
        /// </summary>
        protected virtual void Destory() { }
        /// <summary>
        /// 已经更新完并且已经画完了
        /// </summary>
        protected virtual void FixUpdate() { }
        public void Dispose()
        {
            Destory();
            SpriteContainer.DestorySprites();
            
        }

    }
}
