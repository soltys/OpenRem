using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace OpenRem.CommonUI
{
    /// <summary>
    /// Template selector used for select data template for specific type.
    /// <para>Selects default template if no type specific one is found.</para>
    /// </summary>
    [ContentProperty("Templates")]
    public class TypeSpecificDataTemplateSelector : DataTemplateSelector
    {
        private readonly Collection<TypeSpecificDataTemplate> templates = new Collection<TypeSpecificDataTemplate>();

        public Collection<TypeSpecificDataTemplate> Templates
        {
            get { return this.templates; }
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null || Templates.Count == 0)
            {
                return null;
            }

            // Default template used when no type specific templates were found
            var defaultTemplate = Templates.FirstOrDefault(template => template != null && template.IsDefault);

            // Try to find type specific template
            var otherTemplates = Templates.Except(new[] {defaultTemplate});
            var foundTemplate = otherTemplates
                .Where(template => template.DataTemplate != null &&
                                   ((Type) template.DataTemplate.DataType).IsInstanceOfType(item))
                .OrderByDescending(template => (Type) template.DataTemplate.DataType, new TypeComparer())
                .FirstOrDefault();

            if (foundTemplate != null)
            {
                return foundTemplate.DataTemplate;
            }

            return defaultTemplate != null ? defaultTemplate.DataTemplate : null;
        }

        private class TypeComparer : IComparer<Type>
        {
            public int Compare(Type x, Type y)
            {
                return x.IsSubclassOf(y) ? 1 : (y.IsSubclassOf(x) ? -1 : 0);
            }
        }
    }
}