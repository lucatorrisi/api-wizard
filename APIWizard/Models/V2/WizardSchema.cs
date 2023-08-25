using APIWizard.Constants;
using APIWizard.Extensions;
using APIWizard.Models.Abstracts;
using APIWizard.Models.Interfaces;
using APIWizard.Utils;
using Newtonsoft.Json;

namespace APIWizard.Models.V2
{
    /// <summary>
    /// Represents a version 2 of the OpenAPI wizard schema.
    /// </summary>
    internal class WizardSchema : WizardSchemaBase, IWizardSchema
    {
        /// <summary>
        /// Gets or sets the host of the API.
        /// </summary>
        [JsonProperty("host")]
        internal string Host { get; set; }
        
        /// <summary>
        /// Gets or sets the base path of the API.
        /// </summary>
        [JsonProperty("basePath")]
        internal string BasePath { get; set; }
        
        /// <summary>
        /// Gets or sets the supported schemes of the API.
        /// </summary>
        [JsonProperty("schemes")]
        internal string[] Schemes { get; set; }
        
        /// <summary>
        /// Gets or sets the paths and their details for the API.
        /// </summary>
        [JsonProperty("paths")]
        public Dictionary<string, Dictionary<string, PathDetail?>>? Paths { get; set; }
        
        /// <inheritdoc/>
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
        
        /// <inheritdoc/>
        public void AddServers(List<string> servers)
        {
            ValidationUtils.ArgumentNotNull(servers, nameof(servers));
            if (servers.Any())
                BasePath = servers.First();
        }
        
        /// <inheritdoc/>
        internal override Uri GetUri(string route)
        {
            return new(HttpRequestUtils.CombineUri(Host, BasePath, route, Schemes));
        }
        
        /// <inheritdoc/>
        internal override Uri GetUri(string server, string route)
        {
            return GetUri(route);
        }
    }
}
