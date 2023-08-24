using APIWizard.Models.Abstracts;
using APIWizard.Extensions;
using Newtonsoft.Json;
using APIWizard.Enums;

namespace APIWizard.Models.V2
{
    internal class PathDetail : PathDetailBase
    {
        [JsonProperty("consumes")]
        internal string[]? Consumes { get; set; }
        [JsonProperty("parameters")]
        public Parameter[]? Parameters { get; set; }

        private Parameter? BodyParameter => Parameters?.FirstOrDefault(p => p.In == Enums.ParameterType.Body);

        internal override string? GetContentType()
        {
            return Consumes?.FirstOrDefault();
        }
        internal override bool HasBodyParameter()
        {
            return BodyParameter != null;
        }
        internal override bool IsBodyRequired()
        {
            return BodyParameter?.Required ?? false;
        }
        internal override void AddDummyBodyParam()
        {
            Parameters?.Add(new Parameter
            {
                In = ParameterType.Body,
                Required = IsBodyRequired()

            });
        }
    }
}
