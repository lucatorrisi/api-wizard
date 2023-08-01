namespace APIWizard
{
    public interface IAPIClient
    {
        /// <summary>
        /// Sends an HTTP GET request and deserializes the response to the specified type.
        /// </summary>
        /// <typeparam name="TResult">The type of the result to deserialize the response to.</typeparam>
        /// <param name="pathName">The API endpoint path.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the request.</param>
        /// <returns>The deserialized response of type TResult.</returns>
        /// <exception cref="ArgumentNullException">Thrown when pathName is null.</exception>
        Task<TResult> SendRequestAsync<TResult>(string pathName, CancellationToken cancellationToken = default);
        /// <summary>
        /// Sends an HTTP GET request and deserializes the response to the specified type with a default result.
        /// </summary>
        /// <typeparam name="TResult">The type of the result to deserialize the response to.</typeparam>
        /// <param name="pathName">The API endpoint path.</param>
        /// <param name="defaultResult">The default result if the deserialization fails or the response is empty.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the request.</param>
        /// <returns>The deserialized response of type TResult, or the default result if deserialization fails or the response is empty.</returns>
        /// <exception cref="ArgumentNullException">Thrown when pathName is null.</exception>
        Task<TResult> SendRequestAsync<TResult>(string pathName, TResult defaultResult = default, CancellationToken cancellationToken = default);
        /// <summary>
        /// Sends an HTTP request with the specified method and deserializes the response to the specified type with a default result.
        /// </summary>
        /// <typeparam name="TResult">The type of the result to deserialize the response to.</typeparam>
        /// <param name="pathName">The API endpoint path.</param>
        /// <param name="method">The HTTP method for the request.</param>
        /// <param name="requestBody">The request body (if any).</param>
        /// <param name="defaultValue">The default result if the deserialization fails or the response is empty.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the request.</param>
        /// <returns>The deserialized response of type TResult, or the default result if deserialization fails or the response is empty.</returns>
        /// <exception cref="ArgumentNullException">Thrown when pathName or method is null.</exception>
        Task<TResult> SendRequestAsync<TResult>(string pathName, string method, object requestBody = null, TResult defaultValue = default, CancellationToken cancellationToken = default);
        /// <summary>
        /// Sends an HTTP request with the specified method, request body, and server, deserializes the response to the specified type with a default result.
        /// </summary>
        /// <typeparam name="TResult">The type of the result to deserialize the response to.</typeparam>
        /// <param name="pathName">The API endpoint path.</param>
        /// <param name="method">The HTTP method for the request.</param>
        /// <param name="requestBody">The request body (if any).</param>
        /// <param name="server">The server address (optional).</param>
        /// <param name="defaultValue">The default result if the deserialization fails or the response is empty.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the request.</param>
        /// <returns>The deserialized response of type TResult, or the default result if deserialization fails or the response is empty.</returns>
        /// <exception cref="ArgumentNullException">Thrown when pathName or method is null.</exception>
        Task<TResult> SendRequestAsync<TResult>(string pathName, string method, object requestBody = null, string server = null, TResult defaultValue = default, CancellationToken cancellationToken = default);
    }
}
