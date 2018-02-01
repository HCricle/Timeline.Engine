using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timeline.Framework.Drawing.Layers
{
    /// <summary>
    /// 规定精灵不能有CanvasCommandList,图层只能有一个
    /// </summary>
    public interface ILayer : IDisposable
    {
        bool IsActive { get; set; }
    }
}
