using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace OpenRem.CommonUI
{
    public class MyWindow : Window
    {
        public object TitlePlaceableContent
        {
            get => (object) GetValue(MyWindow.TitlePlaceableContentProperty);
            set => SetValue(MyWindow.TitlePlaceableContentProperty, value);
        }

        public static readonly DependencyProperty TitlePlaceableContentProperty =
            DependencyProperty.Register("TitlePlaceableContent", typeof(object), typeof(MyWindow),
                new PropertyMetadata(null));

        protected MyWindow()
        {
            CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, OnCloseWindow));
            CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand, OnMaximizeWindow,
                OnCanResizeWindow));
            CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, OnMinimizeWindow,
                OnCanMinimizeWindow));
            CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand, OnRestoreWindow,
                OnCanResizeWindow));
            Loaded += OnWindowLoaded;
        }

        public override void OnApplyTemplate()
        {
            CompatibilityMaximizedNoneWindow(this);
            base.OnApplyTemplate();
        }

        private void OnCanResizeWindow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (ResizeMode == ResizeMode.CanResize || ResizeMode == ResizeMode.CanResizeWithGrip) &&
                           Owner == null;
        }

        private void OnCanMinimizeWindow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ResizeMode != ResizeMode.NoResize && Owner == null;
        }

        private void OnCloseWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        private void OnMaximizeWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MaximizeWindow(this);
        }

        private void OnMinimizeWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void OnRestoreWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.RestoreWindow(this);
        }

        class NativeMethods
        {
            [DllImport("user32")]
            internal static extern bool GetMonitorInfo(IntPtr hMonitor, MONITORINFO lpmi);

            [DllImport("user32")]
            internal static extern IntPtr MonitorFromWindow(IntPtr handle, int flags);

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
            public class MONITORINFO
            {
                public int cbSize = Marshal.SizeOf(typeof(MONITORINFO));
                public RECT rcMonitor = new RECT();
                public RECT rcWork = new RECT();
                public int dwFlags = 0;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct RECT
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct POINT
            {
                public int x;
                public int y;

                public POINT(int x, int y)
                {
                    this.x = x;
                    this.y = y;
                }
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct MINMAXINFO
            {
                public POINT ptReserved;
                public POINT ptMaxSize;
                public POINT ptMaxPosition;
                public POINT ptMinTrackSize;
                public POINT ptMaxTrackSize;
            }
        }


        private void CompatibilityMaximizedNoneWindow(Window window)
        {
            WindowInteropHelper wiHelper = new WindowInteropHelper(window);
            IntPtr handle = wiHelper.Handle;
            HwndSource.FromHwnd(handle).AddHook(
                new HwndSourceHook(CompatibilityMaximizedNoneWindowProc));
        }

        private IntPtr CompatibilityMaximizedNoneWindowProc(
            IntPtr hwnd,
            int msg,
            IntPtr wParam,
            IntPtr lParam,
            ref bool handled)
        {
            switch (msg)
            {
                case 0x0024: // WM_GETMINMAXINFO
                    NativeMethods.MINMAXINFO mmi =
                        (NativeMethods.MINMAXINFO) Marshal.PtrToStructure(lParam, typeof(NativeMethods.MINMAXINFO));

                    // Adjust the maximized size and position
                    // to fit the work area of the correct monitor
                    // int MONITOR_DEFAULTTONEAREST = 0x00000002;
                    IntPtr monitor = NativeMethods.MonitorFromWindow(hwnd, 0x00000002);

                    if (monitor != IntPtr.Zero)
                    {
                        NativeMethods.MONITORINFO monitorInfo = new NativeMethods.MONITORINFO();
                        NativeMethods.GetMonitorInfo(monitor, monitorInfo);
                        NativeMethods.RECT rcWorkArea = monitorInfo.rcWork;
                        NativeMethods.RECT rcMonitorArea = monitorInfo.rcMonitor;
                        mmi.ptMaxPosition.x =
                            Math.Abs(rcWorkArea.left - rcMonitorArea.left);
                        mmi.ptMaxPosition.y =
                            Math.Abs(rcWorkArea.top - rcMonitorArea.top);
                        mmi.ptMaxSize.x =
                            Math.Abs(rcWorkArea.right - rcWorkArea.left);
                        mmi.ptMaxSize.y =
                            Math.Abs(rcWorkArea.bottom - rcWorkArea.top);
                        mmi.ptMinTrackSize.x = (int) MinWidth;
                        mmi.ptMinTrackSize.y = (int) MinHeight;
                    }

                    Marshal.StructureToPtr(mmi, lParam, true);
                    handled = true;
                    break;
            }

            return (IntPtr) 0;
        }


        void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            HwndSource source = HwndSource.FromHwnd(new WindowInteropHelper(this).Handle);
            source?.AddHook(new HwndSourceHook(WndProc));
        }

        public event EventHandler Resizing;
        public event EventHandler Resized;

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_ENTERSIZEMOVE = 0x0231;
            const int WM_EXITSIZEMOVE = 0x0232;

            if (msg == WM_ENTERSIZEMOVE)
            {
                Resizing?.Invoke(this, EventArgs.Empty);
            }
            else if (msg == WM_EXITSIZEMOVE)
            {
                Resized?.Invoke(this, EventArgs.Empty);
            }

            return IntPtr.Zero;
        }
    }
}