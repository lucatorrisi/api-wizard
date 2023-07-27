using APIWizard.Constants;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace APIWizard.Models
{
    internal class Path
    {
        public PathDetail PathDetail { get; set; }
        public string HttpMethod { get; set; }

        [JsonProperty("get")]
        [ConfigurationKeyName("get")]
        private PathDetail PathDetailGet { get { return default; } set { PathDetail = value; HttpMethod = HttpClientDefaults.GetHttpMethod; } }
        [JsonProperty("post")]
        [ConfigurationKeyName("post")]
        private PathDetail PathDetailPost { get { return default; } set { PathDetail = value; HttpMethod = HttpClientDefaults.PostHttpMethod; } }
        [JsonProperty("put")]
        [ConfigurationKeyName("put")]
        private PathDetail PathDetailPut { get { return default; } set { PathDetail = value; HttpMethod = HttpClientDefaults.PutHttpMethod; } }
        [JsonProperty("patch")]
        [ConfigurationKeyName("patch")]
        private PathDetail PathDetailPatch { get { return default; } set { PathDetail = value; HttpMethod = HttpClientDefaults.PatchHttpMethod; } }
        [JsonProperty("delete")]
        [ConfigurationKeyName("delete")]
        private PathDetail PathDetailDelete { get { return default; } set { PathDetail = value; HttpMethod = HttpClientDefaults.DeleteHttpMethod; } }
        [JsonProperty("head")]
        [ConfigurationKeyName("head")]
        private PathDetail PathDetailHead { get { return default; } set { PathDetail = value; HttpMethod = HttpClientDefaults.HeadHttpMethod; } }
        [JsonProperty("options")]
        [ConfigurationKeyName("options")]
        private PathDetail PathDetailOptions { get { return default; } set { PathDetail = value; HttpMethod = HttpClientDefaults.OptionsHttpMethod; } }
    }
}
