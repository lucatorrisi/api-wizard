namespace APIWizard.Extensions
{
    internal static class ArrayExtensions
    {
        internal static T[] Add<T>(this T[] originalArray, T newItem)
        {
            Array.Resize(ref originalArray, originalArray.Length + 1);
            originalArray[originalArray.Length - 1] = newItem;
            return originalArray;
        }
    }
}
