using APIWizard.Constants;
using APIWizard.Extensions;
using APIWizard.Models.Abstracts;
using APIWizard.Models.Interfaces;
using APIWizard.Utils;
using Newtonsoft.Json;

namespace APIWizard.Models.V3
{
    internal class WizardSchema : WizardSchemaBase, IWizardSchema
    {
        [JsonProperty("servers")]
        internal List<Server>? Servers { get; set; } = new List<Server>();
        [JsonProperty("paths")]
        public Dictionary<string, Dictionary<string, PathDetail?>>? Paths { get; set; }

        public HttpRequestMessage? BuildRequest(string pathName, object? inputData, string method = null, string server = null)
        {
            ValidationUtils.ArgumentNotNull(pathName, nameof(pathName));
            HttpRequestMessage? request = null;

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

                pathDetail.AddDummyBodyParam();

                request = new HttpRequestMessage(
                HttpRequestUtils.ConvertToHttpMethod(method), GetUri(server, pathName))
                .AddInputData(inputData, pathDetail?.GetContentType(), pathDetail?.Parameters, pathDetail?.IsBodyRequired() ?? false);
            }

            return request;
        }

        public void AddServers(List<string> servers)
        {
            ValidationUtils.ArgumentNotNull(servers, nameof(servers));
            Servers?.AddRange(servers.Select(s => new Server
            {
                Url = s
            }));
        }

        internal override Uri GetUri(string route)
        {
            return new(HttpRequestUtils.CombineUri(Servers?.FirstOrDefault()?.Url, route));
        }

        internal override Uri GetUri(string? server, string route)
        {
            if (server == null)
                return GetUri(route);

            return new(HttpRequestUtils.CombineUri(server, route));
        }
    }
}
