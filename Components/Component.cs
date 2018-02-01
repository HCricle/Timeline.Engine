using Microsoft.Graphics.Canvas.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timeline.Framework.Drawing.Object;

namespace Timeline.Framework.Drawing.Components
{
    /// <summary>
    /// 组件基类
    /// </summary>
    public abstract class Component
    {
        /// <summary>
        /// 构建函数
        /// </summary>
        /// <param name="target">附加者</param>
        public Component(ModifiableObject target)
        {
            Target = target;
        }
        /// <summary>
        /// 是否活动
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// 目标对象
        /// </summary>
        public ModifiableObject Target { get; }
        /// <summary>
        /// 帧时间信息
        /// </summary>
        protected CanvasTimingInformation Time;
        private bool isStartup;
        public void OnUpdate(CanvasTimingInformation time)
        {
            if (!isStartup)
            {
                isStartup = true;
                StartUp();
            }
            if (IsActive)
            {
                Time = time;
                Update();
            }
        }
        /// <summary>
        /// 组件初始化
        /// </summary>
        protected virtual void StartUp() { }
        /// <summary>
        /// 组件更新
        /// </summary>
        protected virtual void Update() { }
    }
}
