using System.Windows;

namespace OpenRem.CommonUI
{
    public class LogExtensions
    {
        public static readonly DependencyProperty IsScrollLockOnProperty = DependencyProperty.RegisterAttached(
            "IsScrollLockOn",
            typeof(bool),
            typeof(LogExtensions),
            new FrameworkPropertyMetadata(false)
        );
        public static void SetIsScrollLockOn(DependencyObject element, bool value)
        {
            element.SetValue(LogExtensions.IsScrollLockOnProperty, value);
        }
        public static bool GetIsScrollLockOn(DependencyObject element)
        {
            return (bool)element.GetValue(LogExtensions.IsScrollLockOnProperty);
        }

        public static readonly DependencyProperty ToggleWrapProperty = DependencyProperty.RegisterAttached(
            "ToggleWrap",
            typeof(bool),
            typeof(LogExtensions),
            new FrameworkPropertyMetadata(false)
        );
        public static void SetToggleWrap(DependencyObject element, bool value)
        {
            element.SetValue(LogExtensions.ToggleWrapProperty, value);
        }
        public static bool GetToggleWrap(DependencyObject element)
        {
            return (bool)element.GetValue(LogExtensions.ToggleWrapProperty);
        }
    }
}
