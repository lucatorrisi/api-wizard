using APIWizard.Models.Response;

namespace APIWizard
{
    public interface IAPIClient
    {
        /// <summary>
        /// Sends an HTTP GET request to the specified API endpoint.
        /// </summary>
        /// <param name="pathName">The API endpoint path name.</param>
        /// <param name="cancellationToken">Cancellation token for cancelling the request.</param>
        /// <returns>An <see cref="APIResponse"/> representing the from an API call.</returns>
        Task<APIResponse> SendRequestAsync(string pathName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends an HTTP request with input data to the specified API endpoint.
        /// </summary>
        /// <param name="pathName">The API endpoint path name.</param>
        /// <param name="inputData">The input data to send in the request body.</param>
        /// <param name="cancellationToken">Cancellation token for cancelling the request.</param>
        /// <returns>An <see cref="APIResponse"/> representing the from an API call.</returns>
        Task<APIResponse> SendRequestAsync(string pathName, object inputData, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends an HTTP request to the specified API endpoint using the specified HTTP method.
        /// </summary>
        /// <param name="pathName">The API endpoint path name.</param>
        /// <param name="method">The HTTP method to use for the request.</param>
        /// <param name="cancellationToken">Cancellation token for cancelling the request.</param>
        /// <returns>An <see cref="APIResponse"/> representing the from an API call.</returns>
        Task<APIResponse> SendRequestAsync(string pathName, string method, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends an HTTP request with input data to the specified API endpoint using the specified HTTP method.
        /// </summary>
        /// <param name="pathName">The API endpoint path name.</param>
        /// <param name="method">The HTTP method to use for the request.</param>
        /// <param name="inputData">The input data to send in the request body.</param>
        /// <param name="cancellationToken">Cancellation token for cancelling the request.</param>
        /// <returns>An <see cref="APIResponse"/> representing the from an API call.</returns>
        Task<APIResponse> SendRequestAsync(string pathName, string method, object inputData, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends an HTTP request with input data to the specified API endpoint using the specified HTTP method and server URL.
        /// </summary>
        /// <param name="pathName">The API endpoint path name.</param>
        /// <param name="method">The HTTP method to use for the request.</param>
        /// <param name="inputData">The input data to send in the request body.</param>
        /// <param name="server">The server URL to send the request to.</param>
        /// <param name="cancellationToken">Cancellation token for cancelling the request.</param>
        /// <returns>An <see cref="APIResponse"/> representing the from an API call.</returns>
        Task<APIResponse> SendRequestAsync(string pathName, string method, object inputData, string server, CancellationToken cancellationToken = default);


    }
}
