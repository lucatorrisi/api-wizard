namespace APIWizard.Extensions
{
    /// <summary>
    /// Provides extension methods for arrays.
    /// </summary>
    internal static class ArrayExtensions
    {
        /// <summary>
        /// Adds a new item to the end of the array.
        /// </summary>
        /// <typeparam name="T">The type of the array elements.</typeparam>
        /// <param name="originalArray">The original array.</param>
        /// <param name="newItem">The new item to add.</param>
        /// <returns>A new array with the added item.</returns>
        internal static T[] Add<T>(this T[] originalArray, T newItem)
        {
            Array.Resize(ref originalArray, originalArray.Length + 1);
            originalArray[originalArray.Length - 1] = newItem;
            return originalArray;
        }
    }
}
