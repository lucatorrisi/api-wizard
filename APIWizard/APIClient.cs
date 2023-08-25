using APIWizard.Constants;
using APIWizard.Exceptions;
using APIWizard.Models.Configuration;
using APIWizard.Models.Interfaces;
using APIWizard.Models.Response;
using APIWizard.Utils;
using Newtonsoft.Json;

namespace APIWizard
{
    /// <summary>
    /// Represents the APIWizard client for making HTTP requests.
    /// </summary>
    public class APIClient : IAPIClient
    {
        private readonly HttpClient httpClient;
        private readonly IWizardSchema schema;

        internal APIClient(APIWizardOptions options, IWizardSchema schema)
        {
            var handler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = options.PooledConnectionLifetime,
                UseCookies = false
            };
            httpClient = new HttpClient(handler);
            this.schema = schema;
        }
        /// <inheritdoc/>
        public async Task<APIResponse> SendRequestAsync(string pathName, CancellationToken cancellationToken = default)
        {
            return await ExecuteRequestAsync(pathName, null, null, null, cancellationToken);
        }
        /// <inheritdoc/>
        public async Task<APIResponse> SendRequestAsync(string pathName, object inputData, CancellationToken cancellationToken = default)
        {
            return await ExecuteRequestAsync(pathName, null, inputData, null, cancellationToken);
        }
        /// <inheritdoc/>
        public async Task<APIResponse> SendRequestAsync(string pathName, string method, CancellationToken cancellationToken = default)
        {
            return await ExecuteRequestAsync(pathName, method, null, null, cancellationToken);
        }
        /// <inheritdoc/>
        public async Task<APIResponse> SendRequestAsync(string pathName, string method, object inputData, CancellationToken cancellationToken = default)
        {
            return await ExecuteRequestAsync(pathName, method, inputData, null, cancellationToken);
        }
        /// <inheritdoc/>
        public async Task<APIResponse> SendRequestAsync(string pathName, string method, object inputData, string server, CancellationToken cancellationToken = default)
        {
            return await ExecuteRequestAsync(pathName, method, inputData, server, cancellationToken);
        }

        private async Task<APIResponse> ExecuteRequestAsync(
            string pathName,
            string method,
            object? inputData,
            string? server,
            CancellationToken cancellationToken = default)
        {
            ValidationUtils.ArgumentNotNullOrEmpty(pathName, nameof(pathName), ExceptionMessages.InvalidPathName);

            if (httpClient == null)
            {
                throw new InvalidOperationException(ExceptionMessages.HttpClientNotInitialized);
            }

            var requestMessage = schema.BuildRequest(pathName, inputData, method, server)
                                  ?? throw new APIClientException(ExceptionMessages.ErrorBuildingRequest);

            try
            {
                var response = await httpClient.SendAsync(requestMessage, cancellationToken);
                return await HandleResponseAsync(response, cancellationToken);
            }
            catch (HttpRequestException ex)
            {
                throw new APIClientException(ExceptionMessages.ErrorSendingRequest, ex);
            }
        }


        private static async Task<APIResponse> HandleResponseAsync(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var content = JsonConvert.DeserializeObject(jsonResponse);
                return new APIResponse(response, content);
            }
            else
            {
                return new APIResponse(response);
            }
        }
    }
}
