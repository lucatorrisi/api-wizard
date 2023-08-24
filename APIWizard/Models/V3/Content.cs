using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace APIWizard.Models.V3
{
    internal class Content
    {
        internal string Type { get; set; }

        [JsonProperty("application/json")]
        [ConfigurationKeyName("application/json")]
        private object ApplicationJson { get { return default; } set { Type = "application/json"; } }
    }
}
