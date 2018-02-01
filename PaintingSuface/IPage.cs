using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timeline.Framework.Drawing.Layers;

namespace Timeline.Framework.Drawing.PaintingSuface
{
    /// <summary>
    /// 抽象于一页纸该有的东西
    /// </summary>
    interface IPage:IDisposable
    {
        void AddLayer(Layer layer);
        void DestoryLayer(Layer layer);
        void DestoryLayer(string name);
        void DestoryLayer(IEnumerable<Layer> layers);
        void DestoryLayer<TLayer>()
            where TLayer : Layer;
        /// <summary>
        /// 销毁所有图层
        /// </summary>
        void DestoryAllLayer();
        IReadOnlyCollection<Layer> GetLayers();
    }
}
