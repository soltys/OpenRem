namespace OpenRem.Common
{
    /// <summary>
    /// Structure holding a left and a right type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Sideable<T>
    {
        /// <summary>
        /// Type for left side
        /// </summary>
        public T Left { get; set; }

        /// <summary>
        /// Type for right side
        /// </summary>
        public T Right { get; set; }
    }
}