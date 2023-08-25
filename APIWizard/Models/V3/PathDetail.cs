using APIWizard.Models.Abstracts;
using APIWizard.Extensions;
using Newtonsoft.Json;
using APIWizard.Enums;

namespace APIWizard.Models.V3
{
    /// <summary>
    /// Represents detailed path information in OpenAPI version 3.
    /// </summary>
    internal class PathDetail : PathDetailBase
    {
        /// <summary>
        /// Gets or sets the list of parameters associated with the path.
        /// </summary>
        [JsonProperty("parameters")]
        public Parameter[]? Parameters { get; set; }
        
        /// <summary>
        /// Gets or sets the request body information associated with the path.
        /// </summary>
        [JsonProperty("requestBody")]
        internal RequestBody? RequestBody { get; set; }

        /// <inheritdoc/>
        internal override string? GetContentType()
        {
            return RequestBody?.Content?.Types?.FirstOrDefault();
        }
        
        /// <inheritdoc/>
        internal override bool HasBodyParameter()
        {
            return RequestBody != null;
        }
        
        /// <inheritdoc/>
        internal override bool IsBodyRequired()
        {
            return HasBodyParameter() && RequestBody.Required;
        }
        
        /// <inheritdoc/>
        internal override void AddDummyBodyParam()
        {
            if (HasBodyParameter())
            {
                Parameters = Parameters?.Add(new Parameter
                {
                    In = ParameterType.Body,
                    Required = IsBodyRequired()
                });
            }
        }
    }
}
