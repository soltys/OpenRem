using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace OpenRem.CommonUI
{
    public class CenterWindowToOwnerBehavior : Behavior<Window>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += OnLoaded;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Loaded -= OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Application curApp = Application.Current;
            Window mainWindow = curApp.MainWindow;
            AssociatedObject.Left = mainWindow.Left + (mainWindow.Width - AssociatedObject.ActualWidth) / 2;
            AssociatedObject.Top = mainWindow.Top + (mainWindow.Height - AssociatedObject.ActualHeight) / 2;
        }
    }

    public class FlipVerticalyBasedOnMousePostion : Behavior<UIElement>
    {
        private ScaleTransform scaleTransform;
        protected override void OnAttached()
        {
            AssociatedObject.RenderTransformOrigin = new Point(0.5,0.5);
            this.scaleTransform = new ScaleTransform(1, 1, 0, 0);
            AssociatedObject.RenderTransform = this.scaleTransform;
            base.OnAttached();
            Application.Current.MainWindow.PreviewMouseMove += MainWindow_MouseMove;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            Application.Current.MainWindow.PreviewMouseMove -= MainWindow_MouseMove;
        }

        private void MainWindow_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var windowWidth = Application.Current.MainWindow.Width;
            var position = e.GetPosition(Application.Current.MainWindow);
            if (position.X > windowWidth/2)
            {
                this.scaleTransform.ScaleX = -1;
            }
            else
            {
                this.scaleTransform.ScaleX = 1;
            }
        }
    }
}