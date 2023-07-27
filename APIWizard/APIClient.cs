using APIWizard.Constants;
using APIWizard.Exceptions;
using APIWizard.Extensions;
using APIWizard.Models;
using Newtonsoft.Json;
using System.Text;

namespace APIWizard
{
    public class APIClient : IAPIClient
    {
        private readonly HttpClient httpClient;
        private readonly WizardSchema schema;

        internal APIClient(TimeSpan pooledConnectionLifetime, WizardSchema schema)
        {
            var handler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = pooledConnectionLifetime
            };
            httpClient = new HttpClient(handler);
            this.schema = schema;

        }
        public async Task<TResult> DoRequestAsync<TResult>(string pathName, object requestBody, CancellationToken cancellationToken, TResult defaultValue = default)
        {
            if (!schema.Paths.TryGetValue(pathName, out Models.Path? path))
            {
                throw new APIClientException(ExceptionMessages.APIClientRequestNotFound);
            }

            var requestMessage = path.ToHttpRequestMessage(schema.Host, schema.BasePath, pathName, schema.Schemes);

            if (requestBody != null)
            {
                var jsonRequest = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonRequest, Encoding.UTF8, GetConsumesByPathName(pathName));
                requestMessage.Content = content;
            }

            using var response = await httpClient.SendAsync(requestMessage, cancellationToken);
            var jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);

            return JsonConvert.DeserializeObject<TResult>(jsonResponse) ?? defaultValue;
        }
        /// <summary>
        /// Retrieves the "Consumes" media type for the specified API path.
        /// If the API path is not found or the media type is not specified, the default media type is returned.
        /// </summary>
        /// <param name="pathName">The name of the API path.</param>
        /// <returns>The "Consumes" media type for the API path, or the default media type if not specified.</returns>
        private string GetConsumesByPathName(string pathName, string defaultMediaType = HttpClientDefaults.DefaultMediaType)
        {
            if (!schema.Paths.TryGetValue(pathName, out Models.Path? path) || path == null)
            {
                return defaultMediaType;
            }

            var consumes = path.PathDetail.Consumes?.FirstOrDefault(p => p == pathName);
            return string.IsNullOrEmpty(consumes) ? defaultMediaType : consumes;
        }
    }
}
