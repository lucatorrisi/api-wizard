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
    public class APIClient
    {
        private readonly HttpClient httpClient;
        private readonly WizardSchema schema;
        private readonly ConcurrentDictionary<string, HttpRequestMessage> requests;

        internal APIClient(TimeSpan pooledConnectionLifetime, WizardSchema schema, ConcurrentDictionary<string, HttpRequestMessage> requests)
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
            HttpRequestMessage? requestMessage;
            try
            {
                requestMessage = requests[pathName];
            }
            catch (Exception ex)
            {
                throw new APIClientException(ExceptionMessages.APIClientRequestNotFound, ex);
            }

            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, GetConsumesByPathName(pathName));
            using HttpResponseMessage response = await httpClient.SendAsync(requestMessage, cancellationToken);
            return JsonConvert.DeserializeObject<TResult>(await response.Content.ReadAsStringAsync(cancellationToken));
        }

        private string GetConsumesByPathName(string pathName)
        {
            return schema.Paths.FirstOrDefault(p => p.Name == pathName)?.Consumes;
        }
    }
}
