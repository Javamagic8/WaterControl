using System.Text.Json.Serialization;


namespace WaterControl.Data.SmartWater
{
    public class Sensor
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("region")]
        public int Region { get; set; }
        [JsonPropertyName("district")]
        public int District { get; set; }
        [JsonPropertyName("status")]
        public int Status { get; set; }
        [JsonPropertyName("lat")]
        public string Lat { get; set; }
        [JsonPropertyName("lon")]
        public string Lon { get; set; }
        [JsonPropertyName("simkart")]
        public object Simkart { get; set; }
        [JsonPropertyName("code")]
        public string Code { get; set; }
        [JsonPropertyName("data")]
        public SensorInfo SensorInfo { get; set; }
    }

    public class SensorDb
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public object Simkart { get; set; }
        public string Code { get; set; }
        public double Level { get; set; }
        public double Volume { get; set; }
        public string Time { get; set; }
        public int Corec { get; set; }
        public string CreatedAt { get; set; }
    }
}
