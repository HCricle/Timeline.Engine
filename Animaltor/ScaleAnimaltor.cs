using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timeline.Framework.Deformation;
using Timeline.Framework.Drawing.Deformation;
using Timeline.Framework.Drawing.Object;

namespace Timeline.Framework.Drawing.Animaltor
{
    /// <summary>
    /// 膨胀动画
    /// </summary>
    public class ScaleAnimaltor : Animaltor
    {
        public ScaleAnimaltor(ModifiableObject target)
            : base(target)
        {
            
        }
        private float totalSecs;
        private ScaleTransform targetTransfrom;
        private ScaleTransform beginValue, endValue;
        public void SetValue(ScaleTransform end, ScaleTransform begin = null)
        {
            endValue = end;
            beginValue = begin ?? new ScaleTransform(Target.Scale.ScaleX, Target.Scale.ScaleY);
        }
        protected override void StartUp()
        {
            targetTransfrom = Target.Scale;
            totalSecs = (float)Duration.TotalSeconds;//计算一共要多少秒做完
            base.StartUp();
        }
        protected override void Update()
        {
            targetTransfrom.ScaleX += (endValue.ScaleX - beginValue.ScaleX) * ((float)Time.ElapsedTime.TotalSeconds / totalSecs);
            targetTransfrom.ScaleY += (endValue.ScaleY - beginValue.ScaleY) * ((float)Time.ElapsedTime.TotalSeconds / totalSecs);
            if (targetTransfrom.ScaleX >= endValue.ScaleX)
            {
                Complated();
            }
            base.Update();
        }
    }
}
