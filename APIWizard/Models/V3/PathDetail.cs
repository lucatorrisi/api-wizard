using APIWizard.Models.Abstracts;
using APIWizard.Models.Interfaces;
using Newtonsoft.Json;

namespace APIWizard.Models.V3
{
    internal class PathDetail : PathDetailBase
    {
        [JsonProperty("parameters")]
        public Parameter[]? Parameters { get; set; }
        [JsonProperty("requestBody")]
        internal RequestBody? RequestBody { get; set; }

        internal override string? GetContentType()
        {
            return RequestBody?.Content?.Type;
        }
    }
}
