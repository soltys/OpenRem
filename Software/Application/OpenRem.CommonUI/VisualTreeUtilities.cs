using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Media;

namespace OpenRem.CommonUI
{
    public static class VisualTreeUtilities
    {
        /// <summary>
        /// Finds visual children by drilling down the visual tree to find child controls with a specified type.
        /// </summary>
        /// <typeparam name="T">Type of the control(s) to search for</typeparam>
        /// <param name="parent">Visual tree root object to start the search from</param>
        /// <param name="deepSearch">Indicates whether the method should search also inside the returned childeren</param>
        /// <returns>IEnumerable of type T that have the indicated name</returns>
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject parent, bool deepSearch = true) where T : FrameworkElement
        {
            return FindVisualChildrenRecursive<T>(parent, null, true, deepSearch);
        }

        /// <summary>
        /// Finds visual children by drilling down the visual tree to find child controls with a certain name and specified type.
        /// </summary>
        /// <typeparam name="T">Type of the control(s) to search for</typeparam>
        /// <param name="parent">Visual tree root object to start the search from</param>
        /// <param name="childName">Name of the control(s) to search for</param>
        /// <returns>IEnumerable of type T that have the indicated name</returns>
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject parent, string childName) where T : FrameworkElement
        {
            if (String.IsNullOrEmpty(childName))
            {
                throw new ArgumentNullException(nameof(childName), "childName must specify a valid framework element name");
            }
            return FindVisualChildrenRecursive<T>(parent, childName, true);
        }

        /// <summary>
        /// Finds visual children by drilling down the visual tree to find child controls with a certain name, regardless of the type.
        /// </summary>
        /// <param name="parent">Visual tree root object to start the search from</param>
        /// <param name="childName">Name of the control(s) to search for</param>
        /// <returns>IEnumerable of FrameworkElement that have the specified name</returns>
        public static IEnumerable<FrameworkElement> FindVisualChildren(DependencyObject parent, string childName)
        {
            if (String.IsNullOrEmpty(childName))
            {
                throw new ArgumentNullException(nameof(childName), "childName must contain a valid framework element name");
            }
            return FindVisualChildrenRecursive<FrameworkElement>(parent, childName, false);
        }

        /// <summary>
        /// Search for the first element of a certain type in the visual tree.
        /// </summary>
        /// <typeparam name="T">The type of element to find.</typeparam>
        /// <param name="parent">The parent element.</param>
        /// <returns>The first occurance of a child of the indicated type</returns>
        public static T FindVisualChild<T>(DependencyObject parent, Func<DependencyObject, bool> filter = null) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = (Visual)VisualTreeHelper.GetChild(parent, i);
                if (child != null)
                {
                    T correctlyTyped = child as T;
                    if (correctlyTyped != null)
                    {
                        if (filter != null && filter(correctlyTyped))
                        {
                            return correctlyTyped;
                        }
                        if (filter == null)
                        {
                            return correctlyTyped;
                        }
                    }

                    T descendent = FindVisualChild<T>(child);
                    if (descendent != null)
                    {
                        if (filter != null && filter(descendent))
                        {
                            return descendent;
                        }
                        if (filter == null)
                        {
                            return descendent;
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Determines if the specified item has a visual parent of the indicated type.
        /// </summary>
        /// <typeparam name="T">the type of the visual parent we are looking for</typeparam>
        /// <param name="child">the child item whose parent we are checking</param>
        /// <returns>true if the item has a visual parent of the indicated type, false otherwise</returns>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public static bool IsVisualParent<T>(DependencyObject child) where T : DependencyObject
        {
            return FindVisualParent<T>(child) != null;
        }

        /// <summary>
        /// Finds the first visual parent of the provided type for the specified item.
        /// </summary>
        /// <typeparam name="T">the type of the visual parent we are looking for</typeparam>
        /// <param name="child">the child item whose parent we are trying to find</param>
        /// <returns>the visual parent of the indicated type that is the closest ancestor of the item, or null if there is no such parent.</returns>
        public static T FindVisualParent<T>(DependencyObject child, Func<DependencyObject, bool> filter = null) where T : DependencyObject
        {
            var parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null)
                return null;
            var parent = parentObject as T;

            if (filter == null)
            {
                return parent ?? FindVisualParent<T>(parentObject);
            }
            return parent != null && filter(parent) ? parent : FindVisualParent<T>(parentObject, filter);
        }

        /// <summary>
        /// Finds the visual parent of the provided type for the specified item by given name.
        /// </summary>
        /// <typeparam name="T">the type of the visual parent we are looking for</typeparam>
        /// <param name="child">the child item whose parent we are trying to find</param>
        /// <param name="name">Expected name of the parent</param>
        /// <returns>the visual parent of the indicated type that is the closest ancestor of the item with given name, or null if there is no such parent.</returns>
        public static T FindVisualParent<T>(DependencyObject child, string name) where T : FrameworkElement
        {
            var parentObject = FindVisualParent<T>(child);
            if (parentObject == null)
            {
                return null;
            }
            if (parentObject.Name == name)
            {
                return parentObject;
            }
            return FindVisualParent<T>(parentObject, name);
        }

        /// <summary>
        /// Finds the first visual parent of the provided type for the specified item.
        /// </summary>
        /// <param name="child">the child item whose parent we are trying to find</param>
        /// <param name="parentType">the type of the visual parent we are looking for</param>
        /// <returns>the visual parent of the indicated type that is the closest ancestor of the item, or null if there is no such parent.</returns>
        public static DependencyObject FindVisualParent(DependencyObject child, Type parentType)
        {
            var parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null)
                return null;
            if (parentObject.GetType() == parentType)
                return parentObject;
            else
                return FindVisualParent(parentObject, parentType);
        }



        /// <summary>
        /// FindChildrenRecursive does the actual recursive search of the visual tree for children with the indicated name and possibly type.
        /// </summary>
        /// <typeparam name="T">Type of the control(s) to search for</typeparam>
        /// <param name="parent">Visual tree root object to start the search from</param>
        /// <param name="childName">Name of the control(s) to search for. Specify <c>null</c> if you don't care what the child's name is.</param>
        /// <param name="checkType">True to enforce the constraint that the children are of the indicated type, false otherwise</param>
        /// <param name="deepSearch">Indicates whether the method should search also inside the returned childeren</param>
        /// <returns>IEnumerable of type T that match the name</returns>
        private static IEnumerable<T> FindVisualChildrenRecursive<T>(DependencyObject parent, string childName, bool checkType, bool deepSearch = true) where T : FrameworkElement
        {
            Debug.Assert(checkType || typeof(T) == typeof(FrameworkElement), "If you don't want to check the type, you must specify 'FrameworkElement' as the T parameter.");

            if (parent != null)
            {
                int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
                for (int i = 0; i < childrenCount; i++)
                {
                    var child = VisualTreeHelper.GetChild(parent, i) as FrameworkElement;
                    if (child == null)
                    {
                        continue;
                    }
                    // If the child is the requested child type (or we are ignoring the child type), see if it has the desired name.
                    if (child is T || !checkType)
                    {
                        if (childName == null || child.Name == childName)
                        {
                            yield return (T)child;
                            if (!deepSearch)
                            {
                                continue;
                            }
                        }
                    }

                    // recursively drill down the tree
                    var childItems = FindVisualChildrenRecursive<T>(child, childName, checkType, deepSearch);
                    if (childItems != null)
                    {
                        foreach (var childItem in childItems)
                        {
                            yield return childItem;
                        }
                    }
                }
            }
        }
    }
}