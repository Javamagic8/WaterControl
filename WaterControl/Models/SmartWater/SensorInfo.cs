using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WaterControl.Data.SmartWater
{
    public class SensorInfo
    {
        public int Id { get; set; }
        [JsonPropertyName("id")]
        public int SensorId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        public int StationID { get; set; }
        [JsonPropertyName("level")]
        public double Level { get; set; }
        [JsonPropertyName("volume")]
        public double Volume { get; set; }
        [JsonPropertyName("time")]
        public string Time { get; set; }
        [JsonPropertyName("corec")]
        public int Corec { get; set; }
        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; }
        public bool IsChecked { get; set; }
    }
}
