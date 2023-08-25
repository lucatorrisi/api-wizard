using System.Net.Http.Headers;
using System.Net;

namespace APIWizard.Models.Response
{
    /// <summary>
    /// Represents an API response.
    /// </summary>
    public class APIResponse
    {
        /// <summary>
        /// Gets the HttpResponseMessage associated with the API response.
        /// </summary>
        public HttpResponseMessage HttpResponseMessage { get; }

        /// <summary>
        /// Gets the content of the API response.
        /// </summary>
        public object? Content { get; }

        /// <summary>
        /// Gets the HTTP status code of the API response.
        /// </summary>
        public HttpStatusCode StatusCode => HttpResponseMessage.StatusCode;

        /// <summary>
        /// Gets the headers of the API response.
        /// </summary>
        public HttpResponseHeaders Headers => HttpResponseMessage.Headers;

        /// <summary>
        /// Gets the reason phrase of the API response.
        /// </summary>
        public string ReasonPhrase => HttpResponseMessage.ReasonPhrase;

        /// <summary>
        /// Gets a value indicating whether the API response indicates success.
        /// </summary>
        public bool IsSuccessStatusCode => HttpResponseMessage.IsSuccessStatusCode;

        /// <summary>
        /// Initializes a new instance of the <see cref="APIResponse"/> class.
        /// </summary>
        /// <param name="response">The HttpResponseMessage associated with the API response.</param>
        public APIResponse(HttpResponseMessage response)
        {
            HttpResponseMessage = response;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="APIResponse"/> class.
        /// </summary>
        /// <param name="response">The HttpResponseMessage associated with the API response.</param>
        /// <param name="content">The content of the API response.</param>
        public APIResponse(HttpResponseMessage response, object content)
        {
            HttpResponseMessage = response;
            Content = content;
        }

        /// <summary>
        /// Gets the content of the API response, cast to a specified type.
        /// </summary>
        /// <typeparam name="T">The type to cast the content to.</typeparam>
        /// <returns>The content cast to the specified type, or default value if cast is not possible.</returns>
        public T? GetContent<T>()
        {
            return Content is T typedContent ? typedContent : default;
        }
    }
}
