using APIWizard.Models.Abstracts;
using APIWizard.Extensions;
using Newtonsoft.Json;
using APIWizard.Enums;

namespace APIWizard.Models.V2
{
    /// <summary>
    /// Represents a detailed path information in OpenAPI version 2.
    /// </summary>
    internal class PathDetail : PathDetailBase
    {
        /// <summary>
        /// Gets or sets the list of content types that the path consumes.
        /// </summary>
        [JsonProperty("consumes")]
        internal string[]? Consumes { get; set; }
        
        /// <summary>
        /// Gets or sets the list of parameters associated with the path.
        /// </summary>
        [JsonProperty("parameters")]
        public Parameter[]? Parameters { get; set; }
        
        /// <summary>
        /// Gets the parameter representing the body content of the path detail.
        /// </summary>
        private Parameter? BodyParameter => Parameters?.FirstOrDefault(p => p.In == ParameterType.Body);
        
        /// <inheritdoc/>
        internal override string? GetContentType()
        {
            return Consumes?.FirstOrDefault();
        }
        
        /// <inheritdoc/>
        internal override bool HasBodyParameter()
        {
            return BodyParameter != null;
        }
        
        /// <inheritdoc/>
        internal override bool IsBodyRequired()
        {
            return BodyParameter?.Required ?? false;
        }
        
        /// <inheritdoc/>
        internal override void AddDummyBodyParam()
        {
            if (HasBodyParameter())
            {
                Parameters?.Add(new Parameter
                {
                    In = ParameterType.Body,
                    Required = IsBodyRequired()
                });
            }
        }
    }
}
