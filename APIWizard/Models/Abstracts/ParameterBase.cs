using APIWizard.Enums;
using Newtonsoft.Json;

namespace APIWizard.Models.Abstracts
{
    /// <summary>
    /// Represents the base class for API parameters.
    /// </summary>
    internal abstract class ParameterBase
    {
        /// <summary>
        /// Gets or sets the name of the parameter.
        /// </summary>
        [JsonProperty("name")]
        internal string Name { get; set; }

        /// <summary>
        /// Gets or sets the location of the parameter in the API request.
        /// </summary>
        [JsonProperty("in")]
        internal ParameterType In { get; set; }

        /// <summary>
        /// Gets or sets the description of the parameter.
        /// </summary>
        [JsonProperty("description")]
        internal string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the parameter is required.
        /// </summary>
        [JsonProperty("required")]
        internal bool Required { get; set; }

        /// <summary>
        /// Gets or sets the data type of the parameter.
        /// </summary>
        [JsonProperty("type")]
        internal string Type { get; set; }

        /// <summary>
        /// Gets or sets the format of the parameter.
        /// </summary>
        [JsonProperty("format")]
        internal string Format { get; set; }
    }
}
