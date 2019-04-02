using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using Application = System.Windows.Application;
using Button = System.Windows.Controls.Button;


namespace OpenRem.CommonUI
{
    public class MinimizeToTrayButton : Button
    {
        private NotifyIcon trayIcon;

        #region Dependency properties

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title", typeof(string), typeof(MinimizeToTrayButton), new PropertyMetadata(default(string)));

        public string Title
        {
            get { return (string) GetValue(MinimizeToTrayButton.TitleProperty); }
            set { SetValue(MinimizeToTrayButton.TitleProperty, value); }
        }

        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
            "Message", typeof(string), typeof(MinimizeToTrayButton), new PropertyMetadata(default(string)));

        public string Message
        {
            get { return (string) GetValue(MinimizeToTrayButton.MessageProperty); }
            set { SetValue(MinimizeToTrayButton.MessageProperty, value); }
        }

        public static readonly DependencyProperty IconPathProperty = DependencyProperty.Register(
            "IconPath", typeof(string), typeof(MinimizeToTrayButton), new PropertyMetadata(default(string)));

        public string IconPath
        {
            get { return (string) GetValue(MinimizeToTrayButton.IconPathProperty); }
            set { SetValue(MinimizeToTrayButton.IconPathProperty, value); }
        }

        public static readonly DependencyProperty IconToolTipProperty = DependencyProperty.Register(
            "IconToolTip", typeof(string), typeof(MinimizeToTrayButton), new PropertyMetadata(default(string)));

        public string IconToolTip
        {
            get { return (string) GetValue(MinimizeToTrayButton.IconToolTipProperty); }
            set { SetValue(MinimizeToTrayButton.IconToolTipProperty, value); }
        }

        public static readonly DependencyProperty TrayContextMenuProperty = DependencyProperty.Register(
            "TrayContextMenu", typeof(FrameworkElement), typeof(MinimizeToTrayButton), new PropertyMetadata(default(FrameworkElement)));

        public FrameworkElement TrayContextMenu
        {
            get { return (FrameworkElement) GetValue(MinimizeToTrayButton.TrayContextMenuProperty); }
            set { SetValue(MinimizeToTrayButton.TrayContextMenuProperty, value); }
        }

        #endregion Dependency properties

        protected override void OnClick()
        {
            base.OnClick();
            MinimizeToTray();
        }

        private void MinimizeToTray()
        {
            var mainWindow = Application.Current.MainWindow;
            if (mainWindow == null)
            {
                return;
            }

            if (this.trayIcon == null)
            {
                InitializeTray(mainWindow);
            }

            ShowAsTrayIcon(mainWindow);
        }

        private void InitializeTray(Window mainWindow)
        {
            this.trayIcon = new NotifyIcon
            {
                BalloonTipIcon = ToolTipIcon.Info,
                BalloonTipTitle = Title,
                BalloonTipText = Message,
                Text = IconToolTip
            };

            var iconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, IconPath);
            if (File.Exists(iconPath))
            {
                this.trayIcon.Icon = new Icon(iconPath);
            }

            if (TrayContextMenu != null)
            {
                var strip = new ContextMenuStrip();
                var host = new ElementHost
                {
                    Child = TrayContextMenu,
                    Dock = DockStyle.Fill,
                    BackColorTransparent = true
                };
                strip.Items.Add(new ToolStripControlHost(host));
                this.trayIcon.ContextMenuStrip = strip;
            }

            this.trayIcon.MouseDoubleClick += (s, e) =>
            {
                if (mainWindow.WindowState == WindowState.Minimized)
                {
                    mainWindow.WindowState = WindowState.Normal;
                    mainWindow.Activate();
                    mainWindow.ShowInTaskbar = true;
                    this.trayIcon.Visible = false;
                }
                else
                {
                    ShowAsTrayIcon(mainWindow);
                }
            };
        }

        private void ShowAsTrayIcon(Window mainWindow)
        {
            mainWindow.WindowState = WindowState.Minimized;
            mainWindow.ShowInTaskbar = false;
            this.trayIcon.ShowBalloonTip(3000);
            this.trayIcon.Visible = true;
        }
    }
}