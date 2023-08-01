using Newtonsoft.Json;

namespace APIWizard.Models.V3
{
    internal class Server
    {
        [JsonProperty("url")]
        internal string Url { get; set; }
    }
}
