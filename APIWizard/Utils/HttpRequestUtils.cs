using APIWizard.Constants;

namespace APIWizard.Utils
{
    /// <summary>
    /// Utility methods for working with HTTP requests and URIs.
    /// </summary>
    internal class HttpRequestUtils
    {
        /// <summary>
        /// Converts a string representation of an HTTP method to an instance of <see cref="HttpMethod"/>.
        /// </summary>
        /// <param name="method">The string representation of the HTTP method.</param>
        /// <returns>An instance of <see cref="HttpMethod"/>.</returns>
        internal static HttpMethod ConvertToHttpMethod(string method)
        {
            ValidationUtils.ArgumentNotNullOrEmpty(method, nameof(method));
            return new HttpMethod(method);
        }
        
        /// <summary>
        /// Combines host, base path, route, and schemes to create a complete URI.
        /// </summary>
        /// <param name="host">The host part of the URI.</param>
        /// <param name="basePath">The base path of the URI.</param>
        /// <param name="route">The route part of the URI.</param>
        /// <param name="schemes">A collection of preferred schemes for the URI.</param>
        /// <returns>The combined URI.</returns>
        internal static string CombineUri(string host, string basePath, string route, ICollection<string> schemes)
        {
            string scheme = GetPreferredScheme(schemes);
            string combinedUrl = $"{scheme}://{host.Trim('/')}/{basePath.Trim('/')}/{route.Trim('/')}";

            return combinedUrl;
        }
        
        /// <summary>
        /// Combines a server and a route to create a complete URI.
        /// </summary>
        /// <param name="server">The server part of the URI.</param>
        /// <param name="route">The route part of the URI.</param>
        /// <returns>The combined URI.</returns>
        internal static string CombineUri(string? server, string route)
        {
            ValidationUtils.ArgumentNotNullOrEmpty(server, nameof(server));
            ValidationUtils.ArgumentNotNullOrEmpty(route, nameof(route));

            return $"{server.Trim()}/{route.Trim('/')}";
        }
        
        /// <summary>
        /// Determines the preferred scheme based on the provided schemes collection.
        /// </summary>
        /// <param name="schemes">A collection of preferred schemes.</param>
        /// <returns>The preferred scheme.</returns>
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
