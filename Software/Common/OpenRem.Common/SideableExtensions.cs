using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace OpenRem.Common
{
    public static class SideableExtensions
    {
        /// <summary>
        /// Returns the flipped side relative to the input strictside
        /// </summary>
        /// <param name="side">Side to flip</param>
        /// <returns>The flipped side</returns>
        public static Side Flip(this Side side)
        {
            return side == Side.Left ? Side.Right : Side.Left;
        }

        /// <summary>
        /// Returns the value at the specified side:
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="sideable">source of value</param>
        /// <param name="side">Side where to find the value</param>
        /// <returns>The value found at the specified side</returns>
        public static T Value<T>(this Sideable<T> sideable, Side side)
        {
            Contract.Requires(sideable != null);

            return side == Side.Left ? sideable.Left : sideable.Right;
        }

        /// <summary>
        /// Changes a value at the given side
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="sideable">Sideable instance to be modified</param>
        /// <param name="side">A side where the value is to be modified</param>
        /// <param name="value">New value to be set</param>
        public static void Assign<T>(this Sideable<T> sideable, Side side, T value)
        {
            Contract.Requires(sideable != null);
            if (side == Side.Left)
            {
                sideable.Left = value;
            }
            else
            {
                sideable.Right = value;
            }
        }

        /// <summary>
        /// Assigns values from source <see cref="Sideable{T}"/>
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="sideable">Sideable instance to be modified</param>
        /// <param name="source">Sideable source to assign from</param>
        public static void AssignAll<T>(this Sideable<T> sideable, Sideable<T> source)
        {
            Contract.Requires(sideable != null);
            Contract.Requires(source != null);

            if (source.Left != null)
            {
                sideable.Left = source.Left;
            }
            if (source.Right != null)
            {
                sideable.Right = source.Right;
            }
        }

        /// <summary>
        /// Projects values from source <see cref="Sideable{TSource}"/> into new <see cref="Sideable{TResult}"/>
        /// </summary>
        /// <typeparam name="TSource">Type of the source value</typeparam>
        /// <typeparam name="TResult">The type of the value returned by <paramref name="selector" />.</typeparam>
        /// <param name="source">Sideable source</param>
        /// <param name="selector">A transform function to apply to each source element</param>
        public static Sideable<TResult> Select<TSource, TResult>(this Sideable<TSource> source, Func<TSource, TResult> selector)
        {
            return Select(source, (side, value) => selector(value));
        }

        /// <summary>
        /// Projects values from source <see cref="Sideable{TSource}"/> (reaviling side infromation) into new <see cref="Sideable{TResult}"/>
        /// </summary>
        /// <typeparam name="TSource">Type of the source value</typeparam>
        /// <typeparam name="TResult">The type of the value returned by <paramref name="selector" />.</typeparam>
        /// <param name="source">Sideable source</param>
        /// <param name="selector">A transform function to apply to each source element</param>
        public static Sideable<TResult> Select<TSource, TResult>(this Sideable<TSource> source, Func<Side, TSource, TResult> selector)
        {
            Contract.Requires(source != null);
            Contract.Requires(selector != null);

            var result = new Sideable<TResult>();

            if (source.Left != null)
            {
                result.Left = selector(Side.Left, source.Left);
            }

            if (source.Right != null)
            {
                result.Right = selector(Side.Right, source.Right);
            }

            return result;
        }

        /// <summary>
        /// Returns side values for corresponding sides from another <see cref="Sideable{TSource}"/>
        /// </summary>
        /// <typeparam name="TValuesSource">Type of the value</typeparam>
        /// <typeparam name="TSidesSource">Type of the corresponding <see cref="Sideable{TSidesSource}"/></typeparam>
        /// <param name="valuesSource">source of value</param>
        /// <param name="sidesSource">Corresponding <see cref="Sideable{TSidesSource}"/></param>
        /// <returns>The value found at the specified side</returns>
        public static IEnumerable<TValuesSource> ValuesForSides<TValuesSource, TSidesSource>(this Sideable<TValuesSource> valuesSource, Sideable<TSidesSource> sidesSource)
            where TValuesSource : class
            where TSidesSource : class
        {
            return ValuesForSides(valuesSource, sidesSource.StrictSides());
        }

        /// <summary>
        /// Returns side values for corresponding sides from another <see cref="Sideable{TSource}"/>
        /// </summary>
        /// <typeparam name="T">Type of the value</typeparam>
        /// <param name="valuesSource">source of value</param>
        /// <param name="sidesSource">sidesSource</param>
        /// <returns>The value found at the specified side</returns>
        public static IEnumerable<T> ValuesForSides<T>(this Sideable<T> valuesSource, IEnumerable<Side> sidesSource)
        {
            Contract.Requires(valuesSource != null);
            Contract.Requires(sidesSource != null);
            Contract.Requires(sidesSource.Count() == sidesSource.Distinct().Count());

            foreach (var side in sidesSource)
            {
                yield return valuesSource.Value(side);
            }
        }

        /// <summary>
        /// Returns the sideable content as an enumerable
        /// </summary>
        /// <typeparam name="T">Type of the sideable content</typeparam>
        /// <param name="sideable">source sideable</param>
        /// <returns>An IEnumerable containing the sideable content</returns>
        public static IEnumerable<T> AsEnumerable<T>(this Sideable<T> sideable) where T : class
        {
            Contract.Requires(sideable != null);

            if (sideable.Left != null)
                yield return sideable.Left;
            if (sideable.Right != null)
                yield return sideable.Right;
        }

        /// <summary>
        /// Returns an enumerable of sides that has any content.
        /// </summary>
        /// <typeparam name="T">Type of the sideable content</typeparam>
        /// <param name="sideable">source sideable</param>
        /// <returns>An enumerable of sides that has any content.</returns>
        public static IEnumerable<Side> StrictSides<T>(this Sideable<T> sideable) where T : class
        {
            Contract.Requires(sideable != null);

            if (sideable.Left != null)
                yield return Side.Left;
            if (sideable.Right != null)
                yield return Side.Right;
        }

        /// <summary>
        /// Test if a specific side has any content
        /// </summary>
        /// <typeparam name="T">Type of the content</typeparam>
        /// <param name="sideable">Source sideable</param>
        /// <param name="side">Specific side to test</param>
        /// <returns>True if the side has any content otherwise false.</returns>
        public static bool HasSide<T>(this Sideable<T> sideable, Side side) where T : class
        {
            Contract.Requires(sideable != null);

            if (side == Side.Left && sideable.Left != null)
                return true;

            return side == Side.Right && sideable.Right != null;
        }

        /// <summary>
        /// Tests if both sides has any content
        /// </summary>
        /// <typeparam name="T">Type of the content</typeparam>
        /// <param name="sideable">Source sideable</param>
        /// <returns>True if both left and right has any content</returns>
        public static bool HasBoth<T>(this Sideable<T> sideable) where T : class
        {
            Contract.Requires(sideable != null);

            return sideable.Left != null && sideable.Right != null;
        }

        public static bool HasAny<T>(this Sideable<T> sideable, Func<T,bool> predicate, Side[] sides)
        {
            return sides.Any(x => predicate(sideable.Value(x)));
        }

        public static bool HasSide(this Side[] input, Side target)
        {
            return input != null && input.Contains(target);
        }

        public static bool IsBinaural(this Side[] input)
        {
            return input.HasSide(Side.Left) && input.HasSide(Side.Right);
        }
    }
}
