using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIWizard.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    internal enum ParameterType
    {
        None,
        Body,
        FormData,
        Path,
        Query,
        Header,
        Cookie
    }
}
