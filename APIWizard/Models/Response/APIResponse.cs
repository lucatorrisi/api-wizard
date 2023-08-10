using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace APIWizard.Models.Response
{
    public class APIResponse<TResult>
    {
        public HttpResponseMessage HttpResponseMessage { get; }
        public TResult Content { get; }

        public HttpStatusCode StatusCode => HttpResponseMessage.StatusCode;
        public HttpResponseHeaders Headers => HttpResponseMessage.Headers;
        public string ReasonPhrase => HttpResponseMessage.ReasonPhrase;
        public bool IsSuccessStatusCode => HttpResponseMessage.IsSuccessStatusCode;

        public APIResponse(HttpResponseMessage response, TResult content)
        {
            HttpResponseMessage = response;
            Content = content;
        }
    }
}
