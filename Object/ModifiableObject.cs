using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timeline.Framework.Drawing.Components;
using Timeline.Framework.Effects;

namespace Timeline.Framework.Drawing.Object
{
    /// <summary>
    /// 表示此对象可以画出来，并且可以修饰(通过Components)
    /// </summary>
    public abstract class ModifiableObject : DrawableObject
    {
        protected ModifiableObject()
        {
            Components = new List<Component>();
        }
        /// <summary>
        /// 组件列表
        /// </summary>
        private List<Component> Components { get; }

        /// <summary>
        /// 表示每帧绘画
        /// </summary>
        public abstract void ObjectDraw(ICanvasResourceCreator creator, CanvasAnimatedDrawEventArgs args);
        /// <summary>
        /// 表示每帧更新
        /// </summary>
        public abstract void ObjectUpdate(ICanvasResourceCreator creator, CanvasTimingInformation args);
        public IReadOnlyCollection<Component> GetComponents()
            => Components;
        /// <summary>
        /// 创建并加入一个组件
        /// </summary>
        public TComponent BuildComponent<TComponent>()
            where TComponent:Component
        {
            var com = (TComponent)Activator.CreateInstance(typeof(TComponent), this);
            Components.Add(com);
            return com;
        }
        /// <summary>
        /// 查询获取组件
        /// </summary>
        /// <typeparam name="TComponent">组件类型</typeparam>
        /// <returns>查询后的结果</returns>
        public TComponent GetComponent<TComponent>()
            where TComponent:Component
        {
           var res= Components.Where(c => c is TComponent);
            if (res == null || res.Count() <= 0) 
            {
                throw new ArgumentException("该组件不存在");
            }
            
            return (TComponent)res.ElementAt(0);
        }
        /// <summary>
        /// 移除组件
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        public void RemoveComponent<TComponent>()
            where TComponent : Component
        {
            var res = GetComponent<TComponent>();
            if (res != null)
            {
                lock (Components)
                {
                    Components.Remove(res);
                }
            }
        }
        /// <summary>
        /// 清除所有组件
        /// </summary>
        public void CleanComponent()
        {
            Components.Clear();
        }
    }
}
