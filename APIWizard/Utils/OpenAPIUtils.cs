using APIWizard.Constants;
using APIWizard.Enums;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace APIWizard.Utils
{
    /// <summary>
    /// Utility methods for working with OpenAPI versions.
    /// </summary>
    internal class OpenAPIUtils
    {
        /// <summary>
        /// Gets the OpenAPI version based on the provided input.
        /// </summary>
        /// <param name="input">The input object (can be IConfigurationSection or JSON string).</param>
        /// <returns>The OpenAPI version.</returns>
        internal static OpenAPIVersion GetOpenAPIVersion(object input)
        {
            if (input is IConfigurationSection configSection)
            {
                return GetOpenAPIVersionFromConfiguration(configSection);
            }
            else if (input is string jsonString)
            {
                return GetOpenAPIVersionFromJson(jsonString);
            }
            return OpenAPIVersion.None;
        }

        private static OpenAPIVersion GetOpenAPIVersionFromConfiguration(IConfigurationSection configSection)
        {
            string? swaggerValue = configSection[Common.SwaggerVersionPropertyName];
            string? openAPIValue = configSection[Common.OpenAPIVersionPropertyName];

            if (!string.IsNullOrEmpty(swaggerValue) && swaggerValue.StartsWith(Common.SwaggerVersionPrefix))
            {
                return OpenAPIVersion.V2;
            }
            else if (!string.IsNullOrEmpty(openAPIValue) && openAPIValue.StartsWith(Common.OpenAPIVersionPrefix))
            {
                return OpenAPIVersion.V3;
            }

            return OpenAPIVersion.None;
        }

        private static OpenAPIVersion GetOpenAPIVersionFromJson(string jsonString)
        {
            JObject jsonObject = JObject.Parse(jsonString);
            JToken swaggerToken = jsonObject.GetValue(Common.SwaggerVersionPropertyName);
            JToken openAPIToken = jsonObject.GetValue(Common.OpenAPIVersionPropertyName);

            if (swaggerToken != null && swaggerToken.ToString().StartsWith(Common.SwaggerVersionPrefix))
            {
                return OpenAPIVersion.V2;
            }
            else if (openAPIToken != null && openAPIToken.ToString().StartsWith(Common.OpenAPIVersionPrefix))
            {
                return OpenAPIVersion.V3;
            }

            return OpenAPIVersion.None;
        }
    }
}
