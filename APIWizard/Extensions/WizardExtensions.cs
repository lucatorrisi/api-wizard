using APIWizard.Constants;
using Newtonsoft.Json;
using System.Text;

namespace APIWizard.Extensions
{
    internal static class WizardExtensions
    {
        internal static HttpRequestMessage AddRequestBody(this HttpRequestMessage httpRequestMessage, object? requestBody, string? contentType)
        {
            if (requestBody != null)
            {
                var jsonRequest = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonRequest, Encoding.UTF8, contentType ?? ContentTypes.ApplicationJson);
                httpRequestMessage.Content = content;
            }
            return httpRequestMessage;
        }
    }
}
