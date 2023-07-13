using APIWizard.Model;
using APIWizard.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace APIWizard.Extensions
{
    internal static class WizardExtensions
    {
        internal static HttpRequestMessage ToHttpRequestMessage(this Model.Path path, string host, string basePath, ICollection<string> schemes)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri(HttpRequestUtils.CombineUri(host, basePath, path.Route, schemes));
            request.Method = HttpRequestUtils.ConvertToHttpMethod(path.Method);

            return request;
        }
    }
}
