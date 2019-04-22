using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace OpenRem.CommonUI
{
    public class DoubleClickToClipboardBehavior : Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.MouseLeftButtonDown += OnMouseLeftDown;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.MouseLeftButtonDown -= OnMouseLeftDown;
        }

        public object Target
        {
            get { return (object) GetValue(DoubleClickToClipboardBehavior.TargetProperty); }
            set { SetValue(DoubleClickToClipboardBehavior.TargetProperty, value); }
        }

        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.Register("Target", typeof(object), typeof(DoubleClickToClipboardBehavior),
                new PropertyMetadata(null));


        private void OnMouseLeftDown(object sender, MouseButtonEventArgs e)
        {
            if (Target != null && e.ClickCount == 2 && e.ButtonState == MouseButtonState.Pressed)
            {
                Clipboard.SetText(Target.ToString());
            }
        }
    }
}