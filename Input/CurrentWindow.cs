using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Core;

namespace Timeline.Framework.Input
{
    public static class CurrentWindow
    {
        private static CoreWindow window;

        public static CoreWindow Window => window;
        public static float WindowHeight => (float)Window.Bounds.Height;
        public static float WindowWidth => (float)Window.Bounds.Width;
        //public static CoreWindow Window => CoreWindow.GetForCurrentThread();
        public static void HandleKeyDownEvent(TypedEventHandler<CoreWindow, KeyEventArgs> action)
        {
            Window.KeyDown += action;
        }
        public static void HandleKeyUpEvent(TypedEventHandler<CoreWindow, KeyEventArgs> action)
        {
            Window.KeyUp += action;
        }
        public static void HandlePointerPressedEvent(TypedEventHandler<CoreWindow, PointerEventArgs> action)
        {
            Window.PointerPressed += action;
        }
        public static void HandlePointerReleasedEvent(TypedEventHandler<CoreWindow, PointerEventArgs> action)
        {
            Window.PointerReleased += action;
        }
        public static void HandlePointerEnterEvent(TypedEventHandler<CoreWindow, PointerEventArgs> action)
        {
            Window.PointerEntered += action;
        }
        public static void HandlePointerExiteEvent(TypedEventHandler<CoreWindow, PointerEventArgs> action)
        {
            Window.PointerExited += action;
        }
        static CurrentWindow()
        {
            GetWindow();
            Window.SizeChanged += Window_SizeChanged;
        }
        private static Size windowSize;
        public static Size WindowSize => windowSize;
        private static void Window_SizeChanged(CoreWindow sender, WindowSizeChangedEventArgs args)
        {
            windowSize = args.Size;
        }

        private static void GetWindow()
        {
            window = CoreWindow.GetForCurrentThread();
        }
        public static void RunInWindow(Action action)
        {
            window.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                action.Invoke();
            }).AsTask().Wait();
        }
    }
}
