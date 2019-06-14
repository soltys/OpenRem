using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;

namespace OpenRem.UI
{
    public class GaasControl : ContentControl
    {
        private readonly System.Windows.Forms.Panel panel;
        private readonly WindowsFormsHost host;
        private string ApplicationLocation => Path.Combine(Directory.GetCurrentDirectory(), "gaas\\gaas.exe");

        private Process process;

        public GaasControl()
        {
            this.panel = new System.Windows.Forms.Panel() {Width = 800, Height = 800, Visible = false};
            this.host = new WindowsFormsHost { Child = this.panel };
        }

        public Control MissingApplicationPlaceholder
        {
            get => (Control) GetValue(GaasControl.MissingApplicationPlaceholderProperty);
            set => SetValue(GaasControl.MissingApplicationPlaceholderProperty, value);
        }

        public static readonly DependencyProperty MissingApplicationPlaceholderProperty =
            DependencyProperty.Register(
                "MissingApplicationPlaceholder",
                typeof(Control),
                typeof(GaasControl),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));


        public void Open()
        {
            if (!File.Exists(ApplicationLocation))
            {
                Content = MissingApplicationPlaceholder;
                return;
            }
            else
            {
                Content = this.host;
            }

            ProcessStartInfo psi = new ProcessStartInfo(ApplicationLocation);
            this.process = Process.Start(psi);
            this.process.WaitForInputIdle();

            // Set this as parent, until success, it should have some timeout
            IntPtr handle = IntPtr.Zero;
            while (handle == IntPtr.Zero)
            {
                handle = NativeMethods.SetParent(this.process.MainWindowHandle, this.panel.Handle);
            }
            
            // remove control box
            int style = NativeMethods.GetWindowLong(this.process.MainWindowHandle, NativeMethods.GWL_STYLE);
            style = style & ~NativeMethods.WS_CAPTION & ~NativeMethods.WS_THICKFRAME;
            NativeMethods.SetWindowLong(this.process.MainWindowHandle, NativeMethods.GWL_STYLE, style);

            // resize embedded application & refresh
            ResizeEmbeddedApplication();
        }

        public void Close()
        {
            if (this.process?.HasExited == false)
            {
                this.process.Kill();
            }
        }

        private void ResizeEmbeddedApplication()
        {
            if (this.process == null)
                return;

            NativeMethods.SetWindowPos(this.process.MainWindowHandle, IntPtr.Zero, 0, 0, (int)this.panel.ClientSize.Width, (int)this.panel.ClientSize.Height, NativeMethods.SWP_NOZORDER | NativeMethods.SWP_NOACTIVATE);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            Size size = base.MeasureOverride(availableSize);
            ResizeEmbeddedApplication();
            return size;
        }
    }
}