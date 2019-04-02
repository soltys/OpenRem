using System;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Threading;

namespace OpenRem.CommonUI
{
    public class WindowResizingTrigger : TriggerBase<FrameworkElement>
    {
        private readonly DispatcherTimer timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(500),
        };

        public SizeChange Change { get; set; }

        protected override void OnAttached()
        {
            base.OnAttached();

            if (AssociatedObject.IsLoaded)
            {
                SubscribeToWindowEvents();
            }
            else
            {
                AssociatedObject.Loaded += OnAssosiatedObjectLoaded;
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Loaded -= OnAssosiatedObjectLoaded;
            UnsubscribeFromWindowEvents();
        }

        private void OnAssosiatedObjectLoaded(object sender, RoutedEventArgs e)
        {
            SubscribeToWindowEvents();
        }

        private void SubscribeToWindowEvents()
        {
            var window = VisualTreeUtilities.FindVisualParent<MyWindow>(AssociatedObject);

            window.Resizing += OnResizing;
            window.Resized += OnResized;
            window.StateChanged += OnStateChanged;
            AssociatedObject.SizeChanged += OnAssociatedObjectSizeChanged;
        }

        private void OnAssociatedObjectSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Notify();
        }

        private void OnResized(object sender, EventArgs e)
        {
            Finished();
        }

        private void Finished()
        {
            if (Change == SizeChange.Finished)
            {
                Dispatcher.BeginInvoke(new Action(() => { InvokeActions(SizeChange.Finished); }),
                    DispatcherPriority.Background);
            }
        }

        private void OnResizing(object sender, EventArgs e)
        {
            Started();
        }

        private void Started()
        {
            if (Change == SizeChange.Started)
            {
                InvokeActions(SizeChange.Started);
            }
        }

        private void OnStateChanged(object sender, EventArgs e)
        {
            Notify();
        }

        private void Notify()
        {
            Started();
            this.timer.Tick -= OnTick;
            this.timer.Tick += OnTick;
            this.timer.Stop();
            this.timer.Start();
        }

        private void OnTick(object sender, EventArgs e)
        {
            this.timer.Tick -= OnTick;
            this.timer.Stop();
            Dispatcher.BeginInvoke(new Action(() => { InvokeActions(SizeChange.Finished); }), DispatcherPriority.Background);
        }

        private void UnsubscribeFromWindowEvents()
        {
            var window = VisualTreeUtilities.FindVisualParent<MyWindow>(AssociatedObject);

            window.Resizing -= OnResizing;
            window.Resized -= OnResized;
            window.StateChanged -= OnStateChanged;
            this.timer.Tick -= OnTick;
            AssociatedObject.SizeChanged -= OnAssociatedObjectSizeChanged;
        }
    }
}