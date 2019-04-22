using System.Windows;
using System.Windows.Markup;

namespace OpenRem.CommonUI
{
    [ContentProperty("DataTemplate")]
    public class TypeSpecificDataTemplate : DependencyObject
    {
        public bool IsDefault { get; set; }

        public static readonly DependencyProperty DataTemplateProperty = DependencyProperty.Register(
            "DataTemplate", typeof(DataTemplate), typeof(TypeSpecificDataTemplate),
            new PropertyMetadata(default(DataTemplate)));

        public DataTemplate DataTemplate
        {
            get { return (DataTemplate) GetValue(TypeSpecificDataTemplate.DataTemplateProperty); }
            set { SetValue(TypeSpecificDataTemplate.DataTemplateProperty, value); }
        }
    }
}