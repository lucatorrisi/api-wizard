using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIWizard.Utils
{
    internal static class ValidationUtils
    {
        internal static void ArgumentNotNull([NotNull] object? value, string parameterName, string message = null)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName, message);
            }
        }
        internal static void ArgumentNotNullOrEmpty([NotNull] string value, string parameterName, string message = null)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException(message, parameterName);
            }
        }
    }
}
