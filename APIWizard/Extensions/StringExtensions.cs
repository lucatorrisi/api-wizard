using APIWizard.Constants;
using System.Text.RegularExpressions;

namespace APIWizard.Extensions
{
    /// <summary>
    /// Provides extension methods for strings.
    /// </summary>
    internal static class StringExtensions
    {
        /// <summary>
        /// Checks whether the input string contains curly braces.
        /// </summary>
        /// <param name="inputString">The input string to check.</param>
        /// <returns><c>true</c> if the string contains curly braces, otherwise <c>false</c>.</returns>
        internal static bool ContainsCurlyBraces(this string inputString)
        {
            if (inputString == null)
            {
                return false;
            }
            return Regex.IsMatch(inputString, Common.CurlyBracesPattern);
        }
    }
}
