using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;

namespace OpenRem.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.NotepadControl.Open();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.NotepadControl.Close();
            base.OnClosing(e);
        }
    }

    public class NotepadControl : ContentControl
    {
        private System.Windows.Forms.Panel _panel;
        private Process _process;
        private WindowsFormsHost host;

        public NotepadControl()
        {
            this._panel = new System.Windows.Forms.Panel() {Width = 800, Height = 800};
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

        private const int SWP_NOZORDER = 0x0004;
        private const int SWP_NOACTIVATE = 0x0010;
        private const int GWL_STYLE = -16;
        private const int WS_CAPTION = 0x00C00000;
        private const int WS_THICKFRAME = 0x00040000;

        public void Open()
        {

            ProcessStartInfo psi = new ProcessStartInfo("audio.exe");
            this._process = Process.Start(psi);
            this._process.WaitForInputIdle();
            SetParent(this._process.MainWindowHandle, this._panel.Handle);

            // remove control box
            int style = GetWindowLong(this._process.MainWindowHandle, NotepadControl.GWL_STYLE);
            style = style & ~NotepadControl.WS_CAPTION & ~NotepadControl.WS_THICKFRAME;
            SetWindowLong(this._process.MainWindowHandle, NotepadControl.GWL_STYLE, style);

            // resize embedded application & refresh
            ResizeEmbeddedApp();
        }



        public void Close()
        {
            if (this._process != null)
            {
                this._process.Refresh();
                this._process.Close();
            }
        }

        private void ResizeEmbeddedApp()
        {
            if (this._process == null)
                return;

            SetWindowPos(this._process.MainWindowHandle, IntPtr.Zero, 0, 0, (int)this._panel.ClientSize.Width, (int)this._panel.ClientSize.Height, NotepadControl.SWP_NOZORDER | NotepadControl.SWP_NOACTIVATE);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            Size size = base.MeasureOverride(availableSize);
            ResizeEmbeddedApp();
            return size;
        }
    }
}