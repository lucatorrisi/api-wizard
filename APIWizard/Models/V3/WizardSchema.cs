using APIWizard.Constants;
using APIWizard.Extensions;
using APIWizard.Models.Abstracts;
using APIWizard.Models.Interfaces;
using APIWizard.Utils;
using Newtonsoft.Json;

namespace APIWizard.Models.V3
{    /// <summary>
     /// Represents a version 3 of the OpenAPI wizard schema.
     /// </summary>
    internal class WizardSchema : WizardSchemaBase, IWizardSchema
    {
        /// <summary>
        /// Gets or sets the list of servers associated with the schema.
        /// </summary>
        [JsonProperty("servers")]
        internal List<Server>? Servers { get; set; } = new List<Server>();

        /// <summary>
        /// Gets or sets the paths and their details in the schema.
        /// </summary>
        [JsonProperty("paths")]
        public Dictionary<string, Dictionary<string, PathDetail?>>? Paths { get; set; }
        /// <inheritdoc/>
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
                .AddInputData(inputData, pathDetail?.GetContentType(), pathDetail?.Parameters, pathDetail.IsBodyRequired());
            }

            return request;
        }
        /// <inheritdoc/>
        public void AddServers(List<string> servers)
        {
            ValidationUtils.ArgumentNotNull(servers, nameof(servers));
            Servers?.AddRange(servers.Select(s => new Server
            {
                Url = s
            }));
        }
        /// <inheritdoc/>
        internal override Uri GetUri(string route)
        {
            return new(HttpRequestUtils.CombineUri(Servers?.FirstOrDefault()?.Url, route));
        }
        /// <inheritdoc/>
        internal override Uri GetUri(string? server, string route)
        {
            if (server == null)
                return GetUri(route);

            return new(HttpRequestUtils.CombineUri(server, route));
        }
    }
}
