using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace APIWizard.Models.V3
{
    /// <summary>
    /// Represents content information in OpenAPI version 3.
    /// </summary>
    internal class Content
    {
        /// <summary>
        /// Gets the extension data for content types.
        /// </summary>
        [JsonExtensionData]
        private readonly Dictionary<string, JToken>? _contentTypeData = new();
        /// <summary>
        /// Gets the array of supported content types.
        /// </summary>
        internal string[]? Types => _contentTypeData?.Keys.ToArray();
    }
}
