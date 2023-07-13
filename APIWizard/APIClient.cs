using APIWizard.Constants;
using APIWizard.Exceptions;
using APIWizard.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIWizard
{
    public class APIClient : IAPIClient
    {
        private readonly HttpClient httpClient;
        private readonly WizardSchema schema;
        private readonly Dictionary<string, HttpRequestMessage> requests;

        internal APIClient(TimeSpan pooledConnectionLifetime, WizardSchema schema, Dictionary<string, HttpRequestMessage> requests)
        {
            var handler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = pooledConnectionLifetime
            };
            httpClient = new HttpClient(handler);

            this.schema = schema;
            this.requests = requests;
        }

        public async Task<TResult> DoRequestAsync<TResult>(string pathName, object requestBody, CancellationToken cancellationToken)
        {
            if (!requests.TryGetValue(pathName, out HttpRequestMessage? requestMessage))
            {
                throw new APIClientException(ExceptionMessages.APIClientRequestNotFound);
            }

            var jsonRequest = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(jsonRequest, Encoding.UTF8, GetConsumesByPathName(pathName));
            requestMessage.Content = content;

            using var response = await httpClient.SendAsync(requestMessage, cancellationToken);
            var jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);

            return JsonConvert.DeserializeObject<TResult>(jsonResponse);
        }

        private string GetConsumesByPathName(string pathName)
        {
            var consumes = schema.Paths.FirstOrDefault(p => p.Name == pathName)?.Consumes;
            return string.IsNullOrEmpty(consumes) ? HttpClientDefaults.DefaultMediaType : consumes;
        }
    }
}
