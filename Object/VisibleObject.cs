using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timeline.Framework.Deformation;
using Timeline.Framework.Drawing.Deformation;
using Windows.Foundation;

namespace Timeline.Framework.Drawing.Object
{
    /// <summary>
    /// 表示此对象可见
    /// </summary>
    public abstract class VisibleObject : IVisualObject
    {
        protected VisibleObject()
        {
            Transform = new Transform();
            Scale = new ScaleTransform();
        }
        /// <summary>
        /// 获取物体方框（已进行平移和缩放）
        /// </summary>
        public Rect Bound => new Rect(X + Transform.X, Y + Transform.Y, Width*Scale.ScaleX, Height*Scale.ScaleY);
        /// <summary>
        /// 平移
        /// </summary>
        public Transform Transform { get;}
        /// <summary>
        /// 缩放
        /// </summary>
        public ScaleTransform Scale { get; }
        /// <summary>
        /// 标签
        /// </summary>
        public string Tag { get; set; }
        /// <summary>
        /// 距离屏幕左上角的X位置
        /// </summary>
        public float X { get; set; }
        /// <summary>
        /// 距离屏幕右上角的Y位置
        /// </summary>
        public float Y { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
        public float Height { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        public float Width { get; set; }
        /// <summary>
        /// 此物体是否活动
        /// </summary>
        public bool IsActive { get; set; }
    }
}
