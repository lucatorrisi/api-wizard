using APIWizard.Constants;
using APIWizard.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIWizard.Utils
{
    internal class HttpRequestUtils
    {
        internal static HttpMethod ConvertToHttpMethod(string methodString)
        {
            if (string.Equals(methodString, "GET", StringComparison.OrdinalIgnoreCase))
            {
                return HttpMethod.Get;
            }
            else if (string.Equals(methodString, "POST", StringComparison.OrdinalIgnoreCase))
            {
                return HttpMethod.Post;
            }
            else if (string.Equals(methodString, "PUT", StringComparison.OrdinalIgnoreCase))
            {
                return HttpMethod.Put;
            }
            else if (string.Equals(methodString, "DELETE", StringComparison.OrdinalIgnoreCase))
            {
                return HttpMethod.Delete;
            }
            else
            {
                throw new ArgumentException("Invalid HTTP method", nameof(methodString));
            }
        }

        internal static string CombineUri(string host, string basePath, string route, ICollection<string> schemes)
        {
            string scheme = GetPreferredScheme(schemes);
            host = host.Trim('/');
            basePath = basePath.Trim('/');
            route = route.Trim('/');

            string combinedUrl = $"{scheme}://{host}/{basePath}/{route}";

            return combinedUrl;
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
