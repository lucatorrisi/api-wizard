using APIWizard.Constants;

namespace APIWizard.Utils
{
    internal class HttpRequestUtils
    {
        /// <summary>
        /// Converts the specified HTTP method string to an instance of HttpMethod.
        /// </summary>
        /// <param name="method">The HTTP method string (e.g., "GET", "POST", etc.).</param>
        /// <returns>An instance of HttpMethod representing the specified HTTP method.</returns>
        internal static HttpMethod ConvertToHttpMethod(string method)
        {
            if (string.IsNullOrEmpty(method))
            {
                throw new ArgumentNullException(nameof(method));
            }
            return new HttpMethod(method);
        }
        /// <summary>
        /// Combines the host, base path, and route to create the full URI of the API endpoint.
        /// </summary>
        /// <param name="host">The API host.</param>
        /// <param name="basePath">The base path of the API.</param>
        /// <param name="route">The specific route or path of the API endpoint.</param>
        /// <param name="schemes">A collection of supported schemes (HTTP, HTTPS).</param>
        /// <returns>The combined URI of the API endpoint.</returns>
        internal static string CombineUri(string host, string basePath, string route, ICollection<string> schemes)
        {
            string scheme = GetPreferredScheme(schemes);
            string combinedUrl = $"{scheme}://{host.Trim('/')}/{basePath.Trim('/')}/{route.Trim('/')}";

            return combinedUrl;
        }
        /// <summary>
        /// Determines the preferred scheme (HTTP or HTTPS) based on the available schemes.
        /// If HTTPS is available, it is preferred; otherwise, HTTP is used.
        /// </summary>
        /// <param name="schemes">A collection of supported schemes (HTTP, HTTPS).</param>
        /// <returns>The preferred scheme (HTTP or HTTPS).</returns>
        internal static string GetPreferredScheme(ICollection<string> schemes)
        {
            if (schemes.Any(s => s.Equals(HttpClientDefaults.HttpsSchema, StringComparison.OrdinalIgnoreCase)))
            {
                return HttpClientDefaults.HttpsSchema;
            }
            else
            {
                return HttpClientDefaults.HttpSchema;
            }
        }
    }
}
