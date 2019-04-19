using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace OpenRem.CommonUI
{
    public class AutoScrollOnItemsChange : Behavior<ScrollViewer>
    {
        public ItemsControl Target
        {
            get => (ItemsControl)GetValue(AutoScrollOnItemsChange.TargetProperty);
            set => SetValue(AutoScrollOnItemsChange.TargetProperty, value);
        }

        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.Register("Target", typeof(ItemsControl), typeof(AutoScrollOnItemsChange), new PropertyMetadata(OnTargetChanged));

        private static void OnTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((AutoScrollOnItemsChange)d).UnHook(e.OldValue as ItemsControl);
            ((AutoScrollOnItemsChange)d).Hook();
        }

        public bool ScrollLock
        {
            get => (bool)GetValue(AutoScrollOnItemsChange.ScrollLockProperty);
            set => SetValue(AutoScrollOnItemsChange.ScrollLockProperty, value);
        }

        public static readonly DependencyProperty ScrollLockProperty =
            DependencyProperty.Register("ScrollLock", typeof(bool), typeof(AutoScrollOnItemsChange), new PropertyMetadata(false));


        private void UnHook(ItemsControl itemsControl)
        {
            if (itemsControl?.Items is INotifyCollectionChanged cc)
            {
                cc.CollectionChanged -= OnCollectionChanged;
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            Hook();
        }

        private void Hook()
        {
            if (Target?.Items is INotifyCollectionChanged ic)
            {
                ic.CollectionChanged -= OnCollectionChanged;
                ic.CollectionChanged += OnCollectionChanged;
            }
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (!ScrollLock)
            {
                AssociatedObject?.ScrollToBottom();
            }
        }
    }
}