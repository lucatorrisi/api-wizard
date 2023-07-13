using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIWizard.Sample.Common
{
    public class Forecast
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [JsonProperty("generationtime_ms")]
        public double GenerationTimeMilliseconds { get; set; }
        [JsonProperty("utc_offset_seconds")]
        public double UTCOffsetSeconds { get; set; }
        public string Timezone { get; set; }
        [JsonProperty("timezone_abbreviation")]
        public string TimezoneAbbreviation { get; set; }
        public double Elevation { get; set; }

        public override string ToString()
        {
            return $"Latitude: {Latitude}\n" +
                $"Longitute: {Longitude}\n" +
                $"GenerationTimeMilliseconds: {GenerationTimeMilliseconds}\n" +
                $"UTCOffsetSeconds: {UTCOffsetSeconds}\n" +
                $"Timezone: {Timezone}\n" +
                $"TimezoneAbbreviation: {TimezoneAbbreviation}\n" +
                $"Elevation: {Elevation}";
        }
    }
}
