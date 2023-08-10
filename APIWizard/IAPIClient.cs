using APIWizard.Models.Response;

namespace APIWizard
{
    public interface IAPIClient
    {
        /// <summary>
        /// Sends an HTTP GET request and returns the deserialized API response of the specified type.
        /// </summary>
        /// <typeparam name="TResult">The type of the result to deserialize the response to.</typeparam>
        /// <param name="pathName">The API endpoint path.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the request.</param>
        /// <returns>An <see cref="APIResponse{TResult}"/> containing the deserialized response of type TResult.</returns>
        /// <exception cref="ArgumentNullException">Thrown when pathName is null.</exception>
        Task<APIResponse<TResult>> SendRequestAsync<TResult>(string pathName, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends an HTTP GET request and returns the deserialized API response of the specified type with a default result.
        /// </summary>
        /// <typeparam name="TResult">The type of the result to deserialize the response to.</typeparam>
        /// <param name="pathName">The API endpoint path.</param>
        /// <param name="defaultResult">The default result if deserialization fails or the response is empty.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the request.</param>
        /// <returns>An <see cref="APIResponse{TResult}"/> containing the deserialized response of type TResult, or the default result if deserialization fails or the response is empty.</returns>
        /// <exception cref="ArgumentNullException">Thrown when pathName is null.</exception>
        Task<APIResponse<TResult>> SendRequestAsync<TResult>(string pathName, TResult defaultResult, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends an HTTP request with the specified method and returns the deserialized API response of the specified type.
        /// </summary>
        /// <typeparam name="TResult">The type of the result to deserialize the response to.</typeparam>
        /// <param name="pathName">The API endpoint path.</param>
        /// <param name="method">The HTTP method for the request.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the request.</param>
        /// <returns>An <see cref="APIResponse{TResult}"/> containing the deserialized response of type TResult.</returns>
        /// <exception cref="ArgumentNullException">Thrown when pathName or method is null.</exception>
        Task<APIResponse<TResult>> SendRequestAsync<TResult>(string pathName, string method, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends an HTTP request with the specified method and returns the deserialized API response of the specified type with a default result.
        /// </summary>
        /// <typeparam name="TResult">The type of the result to deserialize the response to.</typeparam>
        /// <param name="pathName">The API endpoint path.</param>
        /// <param name="method">The HTTP method for the request.</param>
        /// <param name="defaultResult">The default result if deserialization fails or the response is empty.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the request.</param>
        /// <returns>An <see cref="APIResponse{TResult}"/> containing the deserialized response of type TResult, or the default result if deserialization fails or the response is empty.</returns>
        /// <exception cref="ArgumentNullException">Thrown when pathName or method is null.</exception>
        Task<APIResponse<TResult>> SendRequestAsync<TResult>(string pathName, string method, TResult defaultResult, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends an HTTP request with the specified method and request body, and returns the deserialized API response of the specified type.
        /// </summary>
        /// <typeparam name="TResult">The type of the result to deserialize the response to.</typeparam>
        /// <param name="pathName">The API endpoint path.</param>
        /// <param name="method">The HTTP method for the request.</param>
        /// <param name="inputData">The input data (if any).</param>
        /// <param name="cancellationToken">A cancellation token to cancel the request.</param>
        /// <returns>An <see cref="APIResponse{TResult}"/> containing the deserialized response of type TResult.</returns>
        /// <exception cref="ArgumentNullException">Thrown when pathName or method is null.</exception>
        Task<APIResponse<TResult>> SendRequestAsync<TResult>(string pathName, string method, object inputData, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends an HTTP request with the specified method, request body, and returns the deserialized API response of the specified type with a default result.
        /// </summary>
        /// <typeparam name="TResult">The type of the result to deserialize the response to.</typeparam>
        /// <param name="pathName">The API endpoint path.</param>
        /// <param name="method">The HTTP method for the request.</param>
        /// <param name="inputData">The input data (if any).</param>
        /// <param name="defaultResult">The default result if deserialization fails or the response is empty.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the request.</param>
        /// <returns>An <see cref="APIResponse{TResult}"/> containing the deserialized response of type TResult, or the default result if deserialization fails or the response is empty.</returns>
        /// <exception cref="ArgumentNullException">Thrown when pathName or method is null.</exception>
        Task<APIResponse<TResult>> SendRequestAsync<TResult>(string pathName, string method, object inputData, TResult defaultResult, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends an HTTP request with the specified method, request body, server address, and returns the deserialized API response of the specified type.
        /// </summary>
        /// <typeparam name="TResult">The type of the result to deserialize the response to.</typeparam>
        /// <param name="pathName">The API endpoint path.</param>
        /// <param name="method">The HTTP method for the request.</param>
        /// <param name="inputData">The input data (if any).</param>
        /// <param name="server">The server address (optional).</param>
        /// <param name="cancellationToken">A cancellation token to cancel the request.</param>
        /// <returns>An <see cref="APIResponse{TResult}"/> containing the deserialized response of type TResult.</returns>
        /// <exception cref="ArgumentNullException">Thrown when pathName or method is null.</exception>
        Task<APIResponse<TResult>> SendRequestAsync<TResult>(string pathName, string method, object inputData, string server, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends an HTTP request with the specified method, request body, server address, and returns the deserialized API response of the specified type with a default result.
        /// </summary>
        /// <typeparam name="TResult">The type of the result to deserialize the response to.</typeparam>
        /// <param name="pathName">The API endpoint path.</param>
        /// <param name="method">The HTTP method for the request.</param>
        /// <param name="inputData">The input data (if any).</param>
        /// <param name="server">The server address (optional).</param>
        /// <param name="defaultResult">The default result if deserialization fails or the response is empty.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the request.</param>
        /// <returns>An <see cref="APIResponse{TResult}"/> containing the deserialized response of type TResult, or the default result if deserialization fails or the response is empty.</returns>
        /// <exception cref="ArgumentNullException">Thrown when pathName or method is null.</exception>
        Task<APIResponse<TResult>> SendRequestAsync<TResult>(string pathName, string method, object inputData, string server, TResult defaultResult, CancellationToken cancellationToken = default);

    }
}
