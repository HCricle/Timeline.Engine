using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Core;
using Windows.UI.ViewManagement;

namespace Timeline.Framework.Input
{
    public class Keyboard
    {
        private static Keyboard instance;
        public static Keyboard GetInstance(CoreWindow window = null)
        {
            if (instance==null)
            {
                if (window==null)
                {
                    throw new ArgumentException("在没有实例前，参数窗口不能为空");
                }
                instance = new Keyboard(window);
            }
            return instance;
        }
        private ConcurrentDictionary<VirtualKey, bool> Keys;
        private Keyboard(CoreWindow window)
        {
            Keys = new ConcurrentDictionary<VirtualKey, bool>();
            var keys = Enum.GetValues(typeof(VirtualKey)).Cast<VirtualKey>();
            for (int i = 0; i < keys.Count(); i++)
            {
                Keys.TryAdd(keys.ElementAt(i), false);
            }
            var cw=ApplicationView.GetForCurrentView();
            window.KeyDown += Win_KeyDown;
            window.KeyUp += Win_KeyUp;
        }
        private event Action<VirtualKey> KeyDown;
        /// <summary>
        /// 注册键按下的事件
        /// </summary>
        /// <param name="action"></param>
        public void RegistKeyDownEvent(Action<VirtualKey> action)
        {
            KeyDown += action;
        }
        /// <summary>
        /// 解除键按下的事件
        /// </summary>
        /// <param name="action"></param>
        public void UnRegistKeyDownEvent(Action<VirtualKey> action)
        {
            KeyDown -= action;
        }
        private void Win_KeyUp(CoreWindow sender, KeyEventArgs args)
        {
            Keys[args.VirtualKey] = false;
        }

        private void Win_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            Keys[args.VirtualKey] = true;
            KeyDown?.Invoke(args.VirtualKey);
        }
        public bool IsKeyDown(VirtualKey key)
        {
              
            return Keys[key];

        }
        public VirtualKey[] GetKeyDowns()
        {
            return Keys.Where(k => k.Value).Select(k=>k.Key).ToArray();
        }
    }
}
