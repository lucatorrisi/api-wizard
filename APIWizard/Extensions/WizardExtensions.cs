using APIWizard.Utils;

namespace APIWizard.Extensions
{
    internal static class WizardExtensions
    {
        internal static HttpRequestMessage ToHttpRequestMessage(this Model.Path path, string host, string basePath, ICollection<string> schemes)
        {
            Uri requestUri = new(HttpRequestUtils.CombineUri(host, basePath, path.Route, schemes));
            HttpMethod httpMethod = HttpRequestUtils.ConvertToHttpMethod(path.Method);

            return new HttpRequestMessage(httpMethod, requestUri);
        }
    }
}
