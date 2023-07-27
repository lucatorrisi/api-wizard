namespace APIWizard
{
    public interface IAPIClient
    {
        /// <summary>
        /// Executes an HTTP request to the specified API path, serializes the request body, and deserializes the response content to the specified type.
        /// If the deserialization result is null, the provided default value will be returned instead.
        /// </summary>
        /// <typeparam name="TResult">The type to deserialize the response content to.</typeparam>
        /// <param name="pathName">The name of the API path.</param>
        /// <param name="requestBody">The request body to be serialized in JSON format.</param>
        /// <param name="cancellationToken">The cancellation token to cancel the request.</param>
        /// <param name="defaultValue">The default value to be returned if deserialization result is null. (Optional)</param>
        /// <returns>The deserialized response content or the default value if the deserialization result is null.</returns>
        Task<TResult> DoRequestAsync<TResult>(string pathName, object requestBody, CancellationToken cancellationToken, TResult defaultValue = default);
    }
}
