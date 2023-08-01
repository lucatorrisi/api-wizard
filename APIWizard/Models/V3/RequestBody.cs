using Newtonsoft.Json;

namespace APIWizard.Models.V3
{
    internal class RequestBody
    {
        [JsonProperty("required")]
        internal bool Required { get; set; }
        [JsonProperty("content")]
        internal Content? Content { get; set; }
    }
}
