using Newtonsoft.Json;

namespace APIWizard.Models.V3
{
    /// <summary>
    /// Represents a request body in API version 3.
    /// </summary>
    internal class RequestBody
    {
        /// <summary>
        /// Gets or sets a value indicating whether the request body is required.
        /// </summary>
        [JsonProperty("required")]
        internal bool Required { get; set; }
        /// <summary>
        /// Gets or sets the content information for the request body.
        /// </summary>
        [JsonProperty("content")]
        internal Content? Content { get; set; }
    }
}
