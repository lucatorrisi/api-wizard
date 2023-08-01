using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIWizard.Sample.Common
{
    public class PetUploadImage
    {
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
