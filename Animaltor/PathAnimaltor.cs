using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timeline.Framework.Deformation;
using Timeline.Framework.Drawing.Object;

namespace Timeline.Framework.Drawing.Animaltor
{
    /// <summary>
    /// 精灵路径动画,由平移实现
    /// </summary>
    public class PathAnimaltor : Animaltor
    {
        public PathAnimaltor(ModifiableObject target)
            : base(target)
        {
        }
        private float totalSecs;
        private Transform targetTransfrom;
        private Transform beginValue,endValue;
        public void SetValue(Transform end,Transform begin=null)
        {
            endValue = end;
            beginValue = begin ?? new Transform(Target.Transform.X, Target.Transform.Y);
        }
        protected override void StartUp()
        {
            targetTransfrom = Target.Transform;
            totalSecs = (float)Duration.TotalSeconds;//计算一共要多少秒做完
            base.StartUp();
        }
        protected override void Update()
        {
            targetTransfrom.X += (endValue.X - beginValue.X) * ((float)Time.ElapsedTime.TotalSeconds / totalSecs);
            targetTransfrom.Y += (endValue.Y - beginValue.Y) * ((float)Time.ElapsedTime.TotalSeconds / totalSecs);
            if (targetTransfrom.X>=endValue.X)
            {
                Complated();
            }
            base.Update();
        }
    }
}
