using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIWizard.Models.Abstracts
{
    internal abstract class ParameterBase
    {
        [JsonProperty("name")]
        internal string Name { get; set; }
        [JsonProperty("in")]
        internal string In { get; set; }
        [JsonProperty("description")]
        internal string Description { get; set; }
        [JsonProperty("required")]
        internal bool Required { get; set; }
        [JsonProperty("type")]
        internal string Type { get; set; }
        [JsonProperty("format")]
        internal string Format { get; set; }
    }
}
