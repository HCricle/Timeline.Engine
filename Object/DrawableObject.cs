using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timeline.Framework.Drawing.Object
{
    /// <summary>
    /// 表示该对象可以画出来
    /// </summary>
    public abstract class DrawableObject: VisibleObject, IDisposable
    {
        /// <summary>
        /// 每帧时间信息
        /// </summary>
        protected CanvasTimingInformation Time;
        /// <summary>
        /// 绘画节
        /// </summary>
        protected CanvasDrawingSession ds;
        /// <summary>
        /// 资源创造器
        /// </summary>
        protected ICanvasResourceCreator ResourceCreator;
        private bool isStartUp;
        public bool IsStartUp => isStartUp;
        public CanvasDrawingSession Ds => ds;
        protected DrawableObject()
        {
        }

        /// <summary>
        /// 初始化的工作
        /// </summary>
        protected virtual void StartUp() { }
        /// <summary>
        /// 每帧更新
        /// </summary>
        protected virtual void Update()
        {
            if (!isStartUp)
            {
                isStartUp = true;
                StartUp();
            }
        }
        /// <summary>
        /// 重新进行初始化，这个不建议用
        /// </summary>
        protected void ReStartUp()
        {
            isStartUp = false;
        }
        /// <summary>
        /// 每帧更新后，（已画完）
        /// </summary>
        protected virtual void FixUpdate() { }
        /// <summary>
        /// 正在销毁时触发
        /// </summary>
        protected virtual void Destory() { }

        /// <summary>
        /// 释放对象资源
        /// </summary>
        public virtual void Dispose()
        {
            Destory();
            GC.SuppressFinalize(this);
        }
    }
}
