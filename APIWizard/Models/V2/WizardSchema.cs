using APIWizard.Constants;
using APIWizard.Extensions;
using APIWizard.Models.Abstracts;
using APIWizard.Models.Interfaces;
using APIWizard.Models.V3;
using APIWizard.Utils;
using Newtonsoft.Json;

namespace APIWizard.Models.V2
{
    internal class WizardSchema : WizardSchemaBase, IWizardSchema
    {
        [JsonProperty("host")]
        internal string Host { get; set; }
        [JsonProperty("basePath")]
        internal string BasePath { get; set; }
        [JsonProperty("schemes")]
        internal string[] Schemes { get; set; }
        [JsonProperty("paths")]
        public Dictionary<string, Dictionary<string, PathDetail?>>? Paths { get; set; }

        public HttpRequestMessage? BuildRequest(string pathName, object? inputData, string method = null, string server = null)
        {
            ValidationUtils.ArgumentNotNull(pathName, nameof(pathName));
            HttpRequestMessage request = null;

            if (Paths == null)
                throw new InvalidOperationException(ExceptionMessages.NoPathsFound);

            if (Paths.TryGetValue(pathName, out var path))
            {
                if (path == null)
                    throw new InvalidOperationException(ExceptionMessages.PathNotFound);

                if (method != null && path.TryGetValue(method, out PathDetail? pathDetail))
                {
                    if (pathDetail == null)
                        throw new InvalidOperationException(ExceptionMessages.MethodNotFound);
                }
                else
                {
                    method = path.FirstOrDefault().Key;
                    pathDetail = path.FirstOrDefault().Value;
                    if (pathDetail == null)
                        throw new InvalidOperationException(ExceptionMessages.NoDefaultMethodFound);
                }

                request = new HttpRequestMessage(
                    HttpRequestUtils.ConvertToHttpMethod(method), GetUri(pathName))
                    .AddInputData(inputData, pathDetail?.GetContentType(), pathDetail?.Parameters, pathDetail.IsBodyRequired());
            }

            return request;
        }

        public void AddServers(List<string> servers)
        {
            ValidationUtils.ArgumentNotNull(servers, nameof(servers));
            if (servers.Any())
                BasePath = servers.First();
        }

        internal override Uri GetUri(string route)
        {
            return new(HttpRequestUtils.CombineUri(Host, BasePath, route, Schemes));
        }

        internal override Uri GetUri(string server, string route)
        {
            return GetUri(route);
        }
    }
}
