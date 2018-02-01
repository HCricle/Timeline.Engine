using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Effects;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Timeline.Framework.Drawing;
using Timeline.Framework.Drawing.Components;
using Timeline.Framework.Drawing.Effects;
using Timeline.Framework.Drawing.Object;
using Windows.Graphics.Effects;

namespace Timeline.Framework.Effects
{
    /// <summary>
    /// TODO:以后要改
    /// </summary>
    /// <typeparam name="TEffect"></typeparam>
    public class CanvasEffect<TEffect> : Component, IEffect
        where TEffect : ICanvasImage, new()
    {

        public CanvasEffect()
            : base(null)
        {
            IsActive = true;
            Effect = new TEffect();
        }
        private PropertyInfo pi;
        public TEffect Effect { get; }
        public void DrawEffect(IGraphicsEffectSource effectSource, CanvasDrawingSession ds)
        {
            pi = Effect.GetType().GetProperty("Source");
            pi.SetValue(Effect, effectSource);
            ds.DrawImage(Effect);
        }
    }
}
