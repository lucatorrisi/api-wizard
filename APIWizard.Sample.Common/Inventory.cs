using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIWizard.Sample.Common
{
    public class Inventory
    {
        [JsonProperty("0")]
        public int Zero { get; set; }
        [JsonProperty("sold")]
        public int Sold { get; set; }
        [JsonProperty("placed")]
        public int Placed { get; set; }
        [JsonProperty("string")]
        public int String { get; set; }
        [JsonProperty("pending")]
        public int Pending { get; set; }
        [JsonProperty("available")]
        public int Available { get; set; }
        [JsonProperty("peric")]
        public int Peric { get; set; }

        public override string ToString()
        {
            return $"Zero: {Zero}\n" +
                $"Sold: {Sold}\n" +
                $"Placed: {Placed}\n" +
                $"String: {String}\n" +
                $"Pending: {Pending}\n" +
                $"Available: {Available}\n" +
                $"Peric: {Peric}";
        }
    }
}
