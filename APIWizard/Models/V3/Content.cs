using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace APIWizard.Models.V3
{
    internal class Content
    {
        [JsonExtensionData]
        private readonly Dictionary<string, JToken>? _contentTypeData = new();

        internal string[]? Types => _contentTypeData?.Keys.ToArray();
    }
}
