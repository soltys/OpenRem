using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace OpenRem.CommonUI
{
    public class TextBoxBehavior : Behavior<TextBox>
    {
        public bool CopyTextToClipboard
        {
            get { return (bool) GetValue(TextBoxBehavior.CopyTextToClipboardProperty); }
            set { SetValue(TextBoxBehavior.CopyTextToClipboardProperty, value); }
        }

        public static readonly DependencyProperty CopyTextToClipboardProperty =
            DependencyProperty.Register("CopyTextToClipboard", typeof(bool), typeof(TextBoxBehavior),
                new PropertyMetadata(false));

        public bool SelectAll
        {
            get { return (bool) GetValue(TextBoxBehavior.SelectAllProperty); }
            set { SetValue(TextBoxBehavior.SelectAllProperty, value); }
        }

        public static readonly DependencyProperty SelectAllProperty = DependencyProperty.Register("SelectAll",
            typeof(bool), typeof(TextBoxBehavior), new PropertyMetadata(false));

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.GotFocus += (s, e) => Update();
            AssociatedObject.GotMouseCapture += (s, e) => Update();
            AssociatedObject.MouseLeftButtonUp += async (s, e) =>
            {
                await Task.Delay(500);
                Update();
            };
        }

        private void Update()
        {
            AssociatedObject.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (SelectAll)
                {
                    AssociatedObject.SelectAll();
                }

                if (CopyTextToClipboard)
                {
                    Clipboard.SetText(AssociatedObject.Text);
                }
            }));
        }
    }
}