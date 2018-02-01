using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timeline.Framework.Drawing.Components;
using Timeline.Framework.Drawing.Object;

namespace Timeline.Framework.Drawing.Animaltor
{
    /// <summary>
    /// 表示一个帧动画
    /// </summary>
    public abstract class Animaltor : Component
    {
        public Animaltor(ModifiableObject target)
            : base(target)
        {
            IsActive = false;//至启动先开始更新
            
        }
        private bool isBegin;
        private TimeSpan duration;
        /// <summary>
        /// 持续时间
        /// </summary>
        public TimeSpan Duration
        {
            get => duration;
            set
            {
                if (IsBegin)
                    throw new Exception("不能在动画启动后设置持续时间");
                duration = value;
            }
        }

        public bool IsBegin => isBegin;
        public event Action BeginAnimaltor;
        public event Action ComplatedAnimaltor;
        /// <summary>
        /// 开始动画,不能在未结束前启动2此
        /// </summary>
        public virtual bool Begin()
        {
            if (IsBegin)
            {
                return false;
            }
            IsActive = true;
            isBegin = true;
            BeginAnimaltor?.Invoke();
            return true;
        }
        /// <summary>
        /// 动画完成
        /// </summary>
        protected void Complated()
        {
            isBegin = false;
            IsActive = false;
            ComplatedAnimaltor?.Invoke();
        }
        /// <summary>
        /// 停止动画
        /// </summary>
        public void Stop()
        {
            Complated();
        }
    }
}
