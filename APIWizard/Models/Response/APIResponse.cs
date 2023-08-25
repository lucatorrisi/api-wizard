using System.Net.Http.Headers;
using System.Net;

namespace APIWizard.Models.Response
{
    public class APIResponse
    {
        public HttpResponseMessage HttpResponseMessage { get; }
        public object? Content { get; }

        public HttpStatusCode StatusCode => HttpResponseMessage.StatusCode;
        public HttpResponseHeaders Headers => HttpResponseMessage.Headers;
        public string ReasonPhrase => HttpResponseMessage.ReasonPhrase;
        public bool IsSuccessStatusCode => HttpResponseMessage.IsSuccessStatusCode;

        public APIResponse(HttpResponseMessage response)
        {
            HttpResponseMessage = response;
        }
        public APIResponse(HttpResponseMessage response, object content)
        {
            HttpResponseMessage = response;
            Content = content;
        }

        public T? GetContent<T>()
        {
            return Content is T typedContent ? typedContent : default;
        }
    }
}
