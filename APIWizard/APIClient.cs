using APIWizard.Constants;
using APIWizard.Exceptions;
using APIWizard.Models.Configuration;
using APIWizard.Models.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace APIWizard
{
    /// <summary>
    /// Represents an API client for making HTTP requests.
    /// </summary>
    public class APIClient : IAPIClient
    {
        private readonly HttpClient httpClient;
        private readonly IWizardSchema schema;

        internal APIClient(APIWizardOptions options, IWizardSchema schema)
        {
            var handler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = options.PooledConnectionLifetime
            };
            httpClient = new HttpClient(handler);
            this.schema = schema;
        }

        public async Task<TResult> SendRequestAsync<TResult>(string pathName, CancellationToken cancellationToken = default)
        {
            return await ExecuteRequestAsync<TResult>(pathName, HttpClientDefaults.GetHttpMethod, cancellationToken: cancellationToken);
        }

        public async Task<TResult> SendRequestAsync<TResult>(string pathName, TResult defaultResult = default, CancellationToken cancellationToken = default)
        {
            return await ExecuteRequestAsync(pathName, HttpClientDefaults.GetHttpMethod, defaultValue: defaultResult, cancellationToken: cancellationToken);
        }

        public async Task<TResult> SendRequestAsync<TResult>(string pathName, string method, object requestBody, string server, TResult defaultValue, CancellationToken cancellationToken)
        {
            return await ExecuteRequestAsync(pathName, method, requestBody, server, defaultValue: defaultValue, cancellationToken: cancellationToken);
        }

        private async Task<TResult> ExecuteRequestAsync<TResult>(string pathName, string method, object requestBody = null, string server = null, TResult defaultValue = default, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(pathName))
                throw new ArgumentNullException(nameof(pathName));
            if (string.IsNullOrEmpty(method))
                throw new ArgumentNullException(nameof(method));

            var requestMessage = schema.BuildRequest(pathName, requestBody, method, server) ?? throw new APIClientException(ExceptionMessages.ErrorBuildingRequest);

            try
            {
                using var response = await httpClient.SendAsync(requestMessage, cancellationToken);
                var jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);
                return JsonConvert.DeserializeObject<TResult>(jsonResponse) ?? defaultValue;
            }
            catch (JsonException ex)
            {
                throw new APIClientException(ExceptionMessages.DeserializationError, ex);
            }
            catch (Exception ex)
            {
                throw new APIClientException(ExceptionMessages.ErrorSendingRequest, ex);
            }
        }
    }
}
