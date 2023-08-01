using APIWizard.Constants;

namespace APIWizard.Utils
{
    internal class HttpRequestUtils
    {
        internal static HttpMethod ConvertToHttpMethod(string method)
        {
            if (string.IsNullOrEmpty(method))
            {
                throw new ArgumentNullException(nameof(method));
            }
            return new HttpMethod(method);
        }
        internal static string CombineUri(string host, string basePath, string route, ICollection<string> schemes)
        {
            string scheme = GetPreferredScheme(schemes);
            string combinedUrl = $"{scheme}://{host.Trim('/')}/{basePath.Trim('/')}/{route.Trim('/')}";

            return combinedUrl;
        }
        internal static string CombineUri(string? server, string route)
        {
            if (string.IsNullOrEmpty(server))
            {
                throw new ArgumentNullException(nameof(server));
            }
            if (string.IsNullOrEmpty(route))
            {
                throw new ArgumentNullException(nameof(route));
            }
            return $"{server.Trim()}/{route.Trim('/')}";
        }
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
