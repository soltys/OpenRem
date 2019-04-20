using System.Windows;

namespace OpenRem.CommonUI
{
    public class ToggleButtonExtensions
    {
        public static readonly DependencyProperty CheckedContentProperty = DependencyProperty.RegisterAttached(
            "CheckedContent",
            typeof(string),
            typeof(ToggleButtonExtensions),
            new FrameworkPropertyMetadata("")
        );
        public static void SetCheckedContent(DependencyObject element, string value)
        {
            element.SetValue(ToggleButtonExtensions.CheckedContentProperty, value);
        }
        public static string GetCheckedContent(DependencyObject element)
        {
            return (string)element.GetValue(ToggleButtonExtensions.CheckedContentProperty);
        }

        public static readonly DependencyProperty UncheckedContentProperty = DependencyProperty.RegisterAttached(
            "UncheckedContent",
            typeof(string),
            typeof(ToggleButtonExtensions),
            new FrameworkPropertyMetadata("")
        );
        public static void SetUncheckedContent(DependencyObject element, string value)
        {
            element.SetValue(ToggleButtonExtensions.UncheckedContentProperty, value);
        }
        public static string GetUncheckedContent(DependencyObject element)
        {
            return (string)element.GetValue(ToggleButtonExtensions.UncheckedContentProperty);
        }
    }
}