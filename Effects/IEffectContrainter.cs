using System.Collections.Generic;

namespace Timeline.Framework.Drawing.Effects
{
    public interface IEffectContrainter
    {
        void AddEffect(IEffect effect);
        void CleanEffect();
        void RemoveEffect(IEffect effect);
        IReadOnlyCollection<IEffect> GetEffects();
    }
}