using Newtonsoft.Json;

namespace APIWizard.Models.V3
{
    /// <summary>
    /// Represents a server in OpenAPI version 3.
    /// </summary>
    internal class Server
    {
        /// <summary>
        /// Gets or sets the URL of the server.
        /// </summary>
        [JsonProperty("url")]
        internal string Url { get; set; }
    }
}
