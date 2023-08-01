using APIWizard.Models.Abstracts;
using APIWizard.Models.Interfaces;
using Newtonsoft.Json;

namespace APIWizard.Models.V2
{
    internal class PathDetail : PathDetailBase
    {
        [JsonProperty("consumes")]
        internal string[]? Consumes { get; set; }
        [JsonProperty("parameters")]
        public Parameter[]? Parameters { get; set; }

        internal override string? GetContentType()
        {
            return Consumes?.FirstOrDefault();
        }
    }
}
