using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Data;

namespace OpenRem.CommonUI
{
    /// <summary>
    /// A dynamic data collection that provides notifications when items
    /// get added, removed, or when the whole list is refreshed.
    /// UICollection provides a fast implementation of AddRange and RemoveRange. 
    /// Use this one any time you bind or any time instead of the built-in <see cref="ObservableCollection&lt;T&gt;"/>.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    public class UICollection<T> : ObservableCollection<T>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public UICollection() { }

        /// <summary>
        /// Initializes a new instance that contains elements copied from the specified collection.
        /// </summary>
        /// <param name="collection">The collection from which the elements are copied.</param>
        /// <exception cref="System.ArgumentNullException">
        /// The collection parameter cannot be null.
        /// </exception>
        public UICollection(IEnumerable<T> collection) : base(collection) { }
        #endregion Constructors

        #region Public methods
        /// <summary>
        /// Adds the elements of the specified range to the end of the collection
        /// </summary>
        /// <param name="range">The range whose elements should be added to the end of the collection</param>
        /// <exception cref="System.ArgumentNullException">
        /// The range parameter cannot be null.
        /// </exception>
        public void AddRange(IEnumerable<T> range)
        {
            var toAddList = range.ToList();
            foreach (var item in toAddList)
            {
                Items.Add(item);
            }
            FireChangedEvents(toAddList, new List<T>());
        }

        /// <summary>
        /// Removes the elements of the specified range from the collection
        /// </summary>
        /// <param name="range">The range whose elements should be removed from the collection</param>
        /// <exception cref="System.ArgumentNullException">
        /// The range parameter cannot be null.
        /// </exception>
        public void RemoveRange(IEnumerable<T> range)
        {
            var removedItems = range.Where(item => Items.Remove(item)).ToList();
            FireChangedEvents(new List<T>(), removedItems);
        }

        /// <summary>
        /// Removed all the items and reinitializes the collection with the specified range
        /// </summary>
        /// <param name="range">The range whose elements should be added to the end of the collection</param>
        /// <exception cref="System.ArgumentNullException">
        /// The range parameter cannot be null.
        /// </exception>
        public void Reset(IEnumerable<T> range)
        {
            var toRemoveList = Items.ToList();
            var toAddList = range.ToList();
            Items.Clear();
            foreach (var item in toAddList)
            {
                Items.Add(item);
            }
            FireChangedEvents(toAddList, toRemoveList);
        }
        #endregion Public methods

        #region CollectionChanged event overrride

        public override event NotifyCollectionChangedEventHandler CollectionChanged;
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            var handlers = CollectionChanged;
            if (handlers != null)
            {
                foreach (var handler in handlers.GetInvocationList().OfType<NotifyCollectionChangedEventHandler>())
                {
                    if (handler.Target is CollectionView)
                    {
                        handler(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                    }
                    else
                    {
                        handler(this, e);
                    }
                }
            }
        }

        #endregion

        #region Private helpers
        /// <summary>
        /// Fires events informing that the whole collection is changed.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void FireChangedEvents(IList newItems, IList oldItems)
        {
            OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(
                NotifyCollectionChangedAction.Replace, newItems, oldItems));
        }
        #endregion Private helpers
    }
}
