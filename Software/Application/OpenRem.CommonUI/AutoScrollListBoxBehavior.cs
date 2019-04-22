using System.Collections.Specialized;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace OpenRem.CommonUI
{
    public class AutoScrollListBoxBehavior : Behavior<ListBox>
    {
        private ScrollViewer scrollViewer;

        private ScrollViewer ScrollViewer
        {
            get
            {
                if (this.scrollViewer == null)
                {
                    this.scrollViewer = VisualTreeUtilities.FindVisualChild<ScrollViewer>(AssociatedObject);
                }

                return this.scrollViewer;
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            ((INotifyCollectionChanged) AssociatedObject.Items).CollectionChanged += OnCollectionChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            ((INotifyCollectionChanged) AssociatedObject.Items).CollectionChanged -= OnCollectionChanged;
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ScrollViewer?.ScrollToEnd();
        }
    }
}