using System.Collections.Generic;
using GalaSoft.MvvmLight;
using OpenRem.Common;
using OpenRem.Core;

namespace OpenRem.CommonUI
{
    public class ObservableSideable<T> : ViewModelBase
    {
        /// <summary>
        /// Creates new instance of ObservableSideable.
        /// </summary>
        /// <param name="left">Value for left side.</param>
        /// <param name="right">Value for right side.</param>
        public ObservableSideable(T left, T right)
        {
            Left = left;
            Right = right;
        }

        /// <summary>
        /// Creates new instance of ObservableSideable.
        /// </summary>
        public ObservableSideable()
        {

        }

        private T left;
        /// <summary>
        /// Value for the <c>Left</c> side.
        /// When changed raises PropertyChanged event.
        /// </summary>
        public T Left
        {
            get => this.left;
            set => Set(ref this.left, value);
        }

        private T right;
        /// <summary>
        /// Value for the <c>Right</c> side.
        /// When changed raises PropertyChanged event.
        /// </summary>
        public T Right
        {
            get => this.right;
            set => Set(ref this.right, value);
        }

        /// <summary>
        /// Indexer used to return value for specified side.
        /// </summary>
        /// <param name="side">Side</param>
        public T this[Side side]
        {
            get => side == Side.Left ? Left : Right;
            set
            {
                if (side == Side.Left)
                {
                    Left = value;
                }
                else
                {
                    Right = value;
                }
            }
        }

        /// <summary>
        /// <c>True</c> if there is a value for the <c>Left</c> side. Otherwise <c>False</c>
        /// </summary>
        public bool HasLeft => Left != null;

        /// <summary>
        /// <c>True</c> if there is a value for the <c>Right</c> side. Otherwise <c>False</c>
        /// </summary>
        public bool HasRight => Right != null;

        /// <summary>
        /// <c>True</c> if there is a value for either the <c>Left</c> or the <c>Right</c> side. Otherwise <c>False</c>
        /// </summary>
        public bool HasAny => HasLeft || HasRight;

        /// <summary>
        /// <c>True</c> if there is a value for both the <c>Left</c> and the <c>Right</c> side. Otherwise <c>False</c>
        /// </summary>
        public bool HasBoth => HasLeft && HasRight;

        /// <summary>
        /// Returns enumeration of all sides where the value is defined.
        /// </summary>
        public IEnumerable<Side> Sides
        {
            get
            {
                if (HasLeft)
                {
                    yield return Side.Left;
                }
                if (HasRight)
                {
                    yield return Side.Right;
                }
            }
        }

        public Sideable<T> ToSideable() => new Sideable<T>
        {
            Left = this.left,
            Right = this.right
        };
    }
}
