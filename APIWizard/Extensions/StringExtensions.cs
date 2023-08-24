using APIWizard.Constants;
using System.Text.RegularExpressions;

namespace APIWizard.Extensions
{
    internal static class StringExtensions
    {
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
