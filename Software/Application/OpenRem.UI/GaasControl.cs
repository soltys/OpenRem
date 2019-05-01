using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;

namespace OpenRem.UI
{
    public class GaasControl : ContentControl
    {
        private System.Windows.Forms.Panel _panel;
        private Process _process;
        private WindowsFormsHost host;

        public GaasControl()
        {
            this._panel = new System.Windows.Forms.Panel() {Width = 800, Height = 800, Visible = false};
            this.host = new WindowsFormsHost { Child = this._panel };
            Content = this.host;
        }

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32")]
        private static extern IntPtr SetParent(IntPtr hWnd, IntPtr hWndParent);

        [DllImport("user32")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);

        [DllImport("kernel32.dll")]
        static extern uint GetLastError();

        private const int SWP_NOZORDER = 0x0004;
        private const int SWP_NOACTIVATE = 0x0010;
        private const int GWL_STYLE = -16;
        private const int WS_CAPTION = 0x00C00000;
        private const int WS_THICKFRAME = 0x00040000;

        public void Open()
        {
            var gaasLocation= Path.Combine(Directory.GetCurrentDirectory(), "gaas\\gaas.exe");
            ProcessStartInfo psi = new ProcessStartInfo(gaasLocation);
            this._process = Process.Start(psi);
            this._process.WaitForInputIdle(3000);

            IntPtr handle = IntPtr.Zero;
            while (handle == IntPtr.Zero)
            {
                handle = SetParent(this._process.MainWindowHandle, this._panel.Handle);
            }
            
            // remove control box
            int style = GetWindowLong(this._process.MainWindowHandle, GaasControl.GWL_STYLE);
            style = style & ~GaasControl.WS_CAPTION & ~GaasControl.WS_THICKFRAME;
            SetWindowLong(this._process.MainWindowHandle, GaasControl.GWL_STYLE, style);

            // resize embedded application & refresh
            ResizeEmbeddedApp();
        }

        public void Close()
        {
            if (!_process.HasExited)
            {
                _process?.Kill();
            }
        }

        private void ResizeEmbeddedApp()
        {
            if (this._process == null)
                return;

            SetWindowPos(this._process.MainWindowHandle, IntPtr.Zero, 0, 0, (int)this._panel.ClientSize.Width, (int)this._panel.ClientSize.Height, GaasControl.SWP_NOZORDER | GaasControl.SWP_NOACTIVATE);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            Size size = base.MeasureOverride(availableSize);
            ResizeEmbeddedApp();
            return size;
        }
    }
}