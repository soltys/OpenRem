using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Threading;

namespace OpenRem.CommonUI
{
    public class SelectAllTextOnFocusBehavior : Behavior<TextBox>
    {
        bool isKeyboardUsed = true;

        public bool SelectAllWhenMadeVisible { get; set; }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.GotKeyboardFocus += AssociatedObject_GotKeyboardFocus;
            AssociatedObject.PreviewMouseLeftButtonUp += OnMouseLeftUp;
            AssociatedObject.IsVisibleChanged += OnIsVisibleChanged;
        }

        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!SelectAllWhenMadeVisible)
            {
                return;
            }
            if (AssociatedObject.Visibility == Visibility.Visible)
            {
                AssociatedObject.Focus();
                AssociatedObject.SelectAll();
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    AssociatedObject.Focus();
                    AssociatedObject.SelectAll();
                }), DispatcherPriority.Background);
            }
        }

        private void OnMouseLeftUp(object sender, MouseButtonEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                AssociatedObject.SelectAll();
            }), DispatcherPriority.Background);
        }

        private void AssociatedObject_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (this.isKeyboardUsed)
            {
                AssociatedObject.SelectAll();
                e.Handled = true;
            }
            this.isKeyboardUsed = true;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.GotKeyboardFocus -= AssociatedObject_GotKeyboardFocus;
            AssociatedObject.PreviewMouseLeftButtonUp -= OnMouseLeftUp;
        }
    }
}