using APIWizard.Utils;

namespace APIWizard.Extensions
{
    internal static class WizardExtensions
    {
        /// <summary>
        /// Converts the specified API path information to an instance of HttpRequestMessage.
        /// </summary>
        /// <param name="path">The API path information to convert.</param>
        /// <param name="host">The API host.</param>
        /// <param name="basePath">The base path of the API.</param>
        /// <param name="route">The specific route or path of the API endpoint.</param>
        /// <param name="schemes">A collection of supported schemes (HTTP, HTTPS).</param>
        /// <returns>An instance of HttpRequestMessage representing the API request.</returns>
        internal static HttpRequestMessage ToHttpRequestMessage(this Models.Path path, string host, string basePath, string route, ICollection<string> schemes)
        {
            Uri requestUri = new(HttpRequestUtils.CombineUri(host, basePath, route, schemes));
            HttpMethod httpMethod = HttpRequestUtils.ConvertToHttpMethod(path.HttpMethod);

            return new HttpRequestMessage(httpMethod, requestUri);
        }
    }
}
