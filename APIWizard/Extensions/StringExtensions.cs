using System.Text.RegularExpressions;

namespace APIWizard.Extensions
{
    public static class StringExtensions
    {
        public static bool ContainsCurlyBraces(this string inputString)
        {
            if (inputString == null)
            {
                return false;
            }
            string pattern = @"\{[^\}]+\}";
            return Regex.IsMatch(inputString, pattern);
        }
    }
}
