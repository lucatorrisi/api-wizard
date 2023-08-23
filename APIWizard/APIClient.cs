using APIWizard.Constants;
using APIWizard.Exceptions;
using APIWizard.Models.Configuration;
using APIWizard.Models.Interfaces;
using APIWizard.Models.Response;
using APIWizard.Utils;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

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

        public async Task<APIResponse<TResult>> SendRequestAsync<TResult>(string pathName, CancellationToken cancellationToken = default)
        {
            return await ExecuteRequestAsync<TResult>(pathName, HttpClientDefaults.GetHttpMethod, null, null, default, cancellationToken: cancellationToken);
        }

        public async Task<APIResponse<TResult>> SendRequestAsync<TResult>(string pathName, TResult defaultResult, CancellationToken cancellationToken = default)
        {
            return await ExecuteRequestAsync(pathName, HttpClientDefaults.GetHttpMethod, null, null, defaultResult, cancellationToken: cancellationToken);
        }

        public async Task<APIResponse<TResult>> SendRequestAsync<TResult>(string pathName, string method, CancellationToken cancellationToken = default)
        {
            return await ExecuteRequestAsync<TResult>(pathName, method, null, null, default, cancellationToken: cancellationToken);
        }

        public async Task<APIResponse<TResult>> SendRequestAsync<TResult>(string pathName, string method, TResult defaultResult, CancellationToken cancellationToken = default)
        {
            return await ExecuteRequestAsync(pathName, method, null, null, defaultResult, cancellationToken: cancellationToken);
        }

        public async Task<APIResponse<TResult>> SendRequestAsync<TResult>(string pathName, string method, object inputData, CancellationToken cancellationToken = default)
        {
            return await ExecuteRequestAsync<TResult>(pathName, method, inputData, null, default, cancellationToken: cancellationToken);
        }

        public async Task<APIResponse<TResult>> SendRequestAsync<TResult>(string pathName, string method, object inputData, TResult defaultResult, CancellationToken cancellationToken = default)
        {
            return await ExecuteRequestAsync(pathName, method, inputData, null, defaultResult, cancellationToken: cancellationToken);
        }

        public async Task<APIResponse<TResult>> SendRequestAsync<TResult>(string pathName, string method, object inputData, string server, CancellationToken cancellationToken = default)
        {
            return await ExecuteRequestAsync<TResult>(pathName, method, inputData, server, default, cancellationToken: cancellationToken);
        }

        public async Task<APIResponse<TResult>> SendRequestAsync<TResult>(string pathName, string method, object inputData, string server, TResult defaultResult, CancellationToken cancellationToken = default)
        {
            return await ExecuteRequestAsync(pathName, method, inputData, server, defaultResult, cancellationToken: cancellationToken);
        }

        private async Task<APIResponse<TResult>> ExecuteRequestAsync<TResult>(
            string pathName,
            string method,
            object? inputData,
            string? server,
            TResult? defaultValue = default,
            CancellationToken cancellationToken = default)
        {
            ValidationUtils.ArgumentNotNullOrEmpty(pathName, nameof(pathName), ExceptionMessages.InvalidPathName);
            ValidationUtils.ArgumentNotNullOrEmpty(method, nameof(method), ExceptionMessages.InvalidMethod);

            if (httpClient == null)
            {
                throw new InvalidOperationException(ExceptionMessages.HttpClientNotInitialized);
            }

            var requestMessage = schema.BuildRequest(pathName, inputData, method, server)
                                  ?? throw new APIClientException(ExceptionMessages.ErrorBuildingRequest);

            try
            {
                var response = await httpClient.SendAsync(requestMessage, cancellationToken);
                return await HandleResponseAsync(response, defaultValue, cancellationToken);
            }
            catch (HttpRequestException ex)
            {
                throw new APIClientException(ExceptionMessages.ErrorSendingRequest, ex);
            }
        }


        private static async Task<APIResponse<TResult>> HandleResponseAsync<TResult>(HttpResponseMessage response, TResult defaultValue, CancellationToken cancellationToken)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var content = JsonConvert.DeserializeObject<TResult>(jsonResponse) ?? defaultValue;
                return new APIResponse<TResult>(response, content);
            }
            else
            {
                return new APIResponse<TResult>(response, defaultValue);
            }
        }
    }
}
