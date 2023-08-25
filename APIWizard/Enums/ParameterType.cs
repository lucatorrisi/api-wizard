using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace APIWizard.Enums
{
    /// <summary>
    /// Represents the type of parameter in an API request.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    internal enum ParameterType
    {
        /// <summary>
        /// No parameter type specified.
        /// </summary>
        None,

        /// <summary>
        /// Parameter appears in the request body.
        /// </summary>
        Body,

        /// <summary>
        /// Parameter appears in the form data of the request.
        /// </summary>
        FormData,
        /// <summary>
        /// Parameter is part of the URL path.
        /// </summary>
        Path,
        /// <summary>
        /// Parameter appears as a query parameter.
        /// </summary>
        Query,
        /// <summary>
        /// Parameter appears in the request header.
        /// </summary>
        Header,
        /// <summary>
        /// Parameter appears as a cookie.
        /// </summary>
        Cookie
    }
}
