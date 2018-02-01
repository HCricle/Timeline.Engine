using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Core;
using System.Collections.Concurrent;
using Windows.UI.Xaml.Input;
using Windows.UI.Input;
using Windows.Foundation;

namespace Timeline.Framework.Input
{
    public class Mouse
    {
        private static Mouse instance;


        public static Mouse GetInstance(CoreWindow window = null)
        {
            if (instance == null)
            {
                if (window == null)
                {
                    //window = CurrentWindow.Window;
                    throw new ArgumentException("在没有实例前，参数窗口不能为空");
                }
                instance = new Mouse(window);
            }
            return instance;
        }
        private Mouse(CoreWindow window)
        {
            window.PointerPressed += Window_PointerPressed;
            window.PointerReleased += Window_PointerReleased;
            window.PointerMoved += Window_PointerMoved;
            KeyModifiers = new ConcurrentDictionary<VirtualKeyModifiers, bool>();
            var kms = Enum.GetValues(typeof(VirtualKeyModifiers)).Cast<VirtualKeyModifiers>();
            for (int i = 0; i < kms.Count(); i++)
            {
                KeyModifiers.TryAdd(kms.ElementAt(i), false);
            }

        }
        private ConcurrentDictionary<VirtualKeyModifiers,bool> KeyModifiers { get; }
        private Point currentPoint;
        private Point pressedPoint;
        private Point releasedPoint;
        private bool isPressed;

        public bool IsPressed => isPressed;
        public Point ReleasedPoint => releasedPoint;
        public Point PressedPoint => pressedPoint;
        public Point CurrentPoint => currentPoint;
        private void Window_PointerMoved(CoreWindow sender, PointerEventArgs args)
        {
            currentPoint = args.CurrentPoint.Position;
        }

        private void Window_PointerReleased(CoreWindow sender, PointerEventArgs args)
        {
            releasedPoint = args.CurrentPoint.Position;
            KeyModifiers[args.KeyModifiers] = false;
            isPressed = false;
        }

        private void Window_PointerPressed(CoreWindow sender, PointerEventArgs args)
        {
            pressedPoint = args.CurrentPoint.Position;
            KeyModifiers[args.KeyModifiers] = true;
            isPressed = true;
        }
        public bool IsKeyModifierPressed(VirtualKeyModifiers virtualKey)
        {
            return KeyModifiers[virtualKey];
        }
        public VirtualKeyModifiers? GetPressedKeyModifier()
        {
            var vkm = KeyModifiers.Where(k => k.Value);
            if (vkm.Count() > 0)
            {
                return vkm.ElementAt(0).Key;
            }
            else
            {
                return null;
            }
        }
    }
}
