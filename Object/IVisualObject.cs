using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timeline.Framework.Drawing.Object
{
    /// <summary>
    /// 表示一个可以见到的物体
    /// </summary>
    public interface IVisualObject
    {
        /// <summary>
        /// 标签
        /// </summary>
        string Tag { get; set; }
        /// <summary>
        /// 离世界轴原点x
        /// </summary>
        float X { get; set; }
        /// <summary>
        /// 离世界轴原点y
        /// </summary>
        float Y { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
        float Height { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        float Width { get; set; }
        /// <summary>
        /// 是否可见
        /// </summary>
        bool IsActive { get; set; }

    }
}
