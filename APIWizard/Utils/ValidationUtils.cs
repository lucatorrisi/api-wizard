using APIWizard.Constants;
using APIWizard.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace APIWizard.Utils
{
    /// <summary>
    /// Utility methods for validating arguments and parameters.
    /// </summary>
    internal static class ValidationUtils
    {
        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the provided value is null.
        /// </summary>
        /// <param name="value">The value to check for null.</param>
        /// <param name="parameterName">The name of the parameter being checked.</param>
        /// <param name="message">The optional error message.</param>
        internal static void ArgumentNotNull([NotNull] object? value, string parameterName, string message = null)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName, message);
            }
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the provided value is null or empty.
        /// </summary>
        /// <param name="value">The value to check for null or empty.</param>
        /// <param name="parameterName">The name of the parameter being checked.</param>
        /// <param name="message">The optional error message.</param>
        internal static void ArgumentNotNullOrEmpty([NotNull] string value, string parameterName, string message = null)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException(message, parameterName);
            }
        }

        /// <summary>
        /// Throws an <see cref="APIClientException"/> if the provided value is null.
        /// </summary>
        /// <param name="value">The value to check for null.</param>
        /// <param name="parameterName">The name of the parameter being checked.</param>
        /// <param name="message">The optional error message.</param>
        internal static void ParameterNotNull([NotNull] object value, string parameterName, string message = ExceptionMessages.MissingRequiredParameter)
        {
            if (value == null)
            {
                throw new APIClientException(string.Format(message, parameterName));
            }
        }

        /// <summary>
        /// Throws an <see cref="APIClientException"/> if the provided value is null or empty.
        /// </summary>
        /// <param name="value">The value to check for null or empty.</param>
        /// <param name="parameterName">The name of the parameter being checked.</param>
        /// <param name="message">The optional error message.</param>
        internal static void ParameterNotNullOrEmpty([NotNull] string value, string parameterName, string message = ExceptionMessages.MissingRequiredParameter)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new APIClientException(string.Format(message, parameterName));
            }
        }
    }
}
