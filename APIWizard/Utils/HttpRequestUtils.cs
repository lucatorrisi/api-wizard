using APIWizard.Constants;
using System.Collections;

namespace APIWizard.Utils
{
    internal class HttpRequestUtils
    {
        internal static HttpMethod ConvertToHttpMethod(string method)
        {
            ValidationUtils.ArgumentNotNullOrEmpty(method, nameof(method));
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
            ValidationUtils.ArgumentNotNullOrEmpty(server, nameof(server));
            ValidationUtils.ArgumentNotNullOrEmpty(route, nameof(route));

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
