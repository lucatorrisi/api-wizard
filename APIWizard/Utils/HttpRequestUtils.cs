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
        internal static HttpMethod ConvertToHttpMethod(string method)
        {
            return new HttpMethod(method);
        }

        internal static string CombineUri(string host, string basePath, string route, ICollection<string> schemes)
        {
            string scheme = GetPreferredScheme(schemes);
            string combinedUrl = $"{scheme}://{host.Trim('/')}/{basePath.Trim('/')}/{route.Trim('/')}";

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
