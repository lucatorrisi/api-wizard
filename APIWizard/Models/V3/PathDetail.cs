using APIWizard.Models.Abstracts;
using APIWizard.Extensions;
using Newtonsoft.Json;
using APIWizard.Enums;

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
        internal override bool HasBodyParameter()
        {
            return RequestBody != null;
        }
        internal override bool IsBodyRequired()
        {
            return HasBodyParameter() && RequestBody.Required;
        }
        internal override void AddDummyBodyParam()
        {
            if(HasBodyParameter())
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
