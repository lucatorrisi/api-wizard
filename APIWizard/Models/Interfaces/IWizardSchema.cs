namespace APIWizard.Models.Interfaces
{
    /// <summary>
    /// Represents the interface for defining API wizard schema methods.
    /// </summary>
    internal interface IWizardSchema
    {
        /// <summary>
        /// Builds an HTTP request message for the specified path, input data, method, and server.
        /// </summary>
        /// <param name="pathName">The name of the API path.</param>
        /// <param name="inputData">The input data for the request.</param>
        /// <param name="method">The HTTP method for the request (optional).</param>
        /// <param name="server">The server for the request (optional).</param>
        /// <returns>The built HttpRequestMessage.</returns>
        internal HttpRequestMessage? BuildRequest(string pathName, object? inputData, string method = null, string server = null);

        /// <summary>
        /// Adds additional servers to the schema.
        /// </summary>
        /// <param name="servers">The list of additional servers to add.</param>
        internal void AddServers(List<string> servers);
    }
}
