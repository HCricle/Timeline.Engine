using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timeline.Framework.Drawing.Effects
{
    public class EffectContrainter : IEffectContrainter
    {
        private List<IEffect> Effects;
        public EffectContrainter()
        {
            Effects = new List<IEffect>();
        }
        public void AddEffect(IEffect effect)
        {
            Effects.Add(effect);
        }
        public void RemoveEffect(IEffect effect)
        {
            var e = Effects.Where(ef => ef == effect);
            if (e==null)
            {
                throw new ArgumentException("不存在此特效");
            }            
            Effects.Remove(effect);
        }
        public void CleanEffect()
        {            
            Effects.Clear();
        }
        public IReadOnlyCollection<IEffect> GetEffects()
            => Effects;
    }
}
