using System.Text.Json.Serialization;

namespace WeatherApp.Models
{
    public class DataModel
    {
        public class WeatherDataModel
        {
            public string Date { get; set; }
            public double Temperature { get; set; }
            public string Description { get; set; }
            public double Humidity { get; set; }
            public double WindSpeed { get; set; }
        }
        public class WeatherResponse
        {
            [JsonPropertyName("currentConditions")]
            public CurrentConditions CurrentConditions { get; set; }

            [JsonPropertyName("days")]
            public List<Day> Days { get; set; }
        }


        public class Day
        {
            [JsonPropertyName("datetime")]
            public string Datetime { get; set; }

            [JsonPropertyName("temp")]
            public float Temp { get; set; }

            [JsonPropertyName("conditions")]
            public string Conditions { get; set; }

            [JsonPropertyName("humidity")]
            public float Humidity { get; set; }

            [JsonPropertyName("windspeed")]
            public float Windspeed { get; set; }
        }
        public class CurrentConditions
        {
            [JsonPropertyName("datetime")]
            public string Datetime { get; set; }

            [JsonPropertyName("datetimeEpoch")]
            public long DatetimeEpoch { get; set; }

            [JsonPropertyName("temp")]
            public float Temp { get; set; }

            [JsonPropertyName("feelslike")]
            public float FeelsLike { get; set; }

            [JsonPropertyName("humidity")]
            public float Humidity { get; set; }

            [JsonPropertyName("dew")]
            public float Dew { get; set; }

            [JsonPropertyName("precip")]
            public float? Precip { get; set; }

            [JsonPropertyName("precipprob")]
            public float? PrecipProb { get; set; }

            [JsonPropertyName("snow")]
            public float? Snow { get; set; }

            [JsonPropertyName("snowdepth")]
            public float? SnowDepth { get; set; }

            [JsonPropertyName("preciptype")]
            public List<string> PrecipType { get; set; }

            [JsonPropertyName("windgust")]
            public float? WindGust { get; set; }

            [JsonPropertyName("windspeed")]
            public float Windspeed { get; set; }

            [JsonPropertyName("winddir")]
            public float WindDir { get; set; }

            [JsonPropertyName("pressure")]
            public float Pressure { get; set; }

            [JsonPropertyName("visibility")]
            public float Visibility { get; set; }

            [JsonPropertyName("cloudcover")]
            public float CloudCover { get; set; }

            [JsonPropertyName("solarradiation")]
            public float? SolarRadiation { get; set; }

            [JsonPropertyName("solarenergy")]
            public float? SolarEnergy { get; set; }

            [JsonPropertyName("uvindex")]
            public float UVIndex { get; set; }

            [JsonPropertyName("conditions")]
            public string Conditions { get; set; }

            [JsonPropertyName("icon")]
            public string Icon { get; set; }

            [JsonPropertyName("stations")]
            public List<string> Stations { get; set; }

            [JsonPropertyName("source")]
            public string Source { get; set; }

            [JsonPropertyName("sunrise")]
            public string Sunrise { get; set; }

            [JsonPropertyName("sunriseEpoch")]
            public long SunriseEpoch { get; set; }

            [JsonPropertyName("sunset")]
            public string Sunset { get; set; }

            [JsonPropertyName("sunsetEpoch")]
            public long SunsetEpoch { get; set; }

            [JsonPropertyName("moonphase")]
            public float MoonPhase { get; set; }
        }



    }

}
