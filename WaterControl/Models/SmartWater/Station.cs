using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WaterControl.Data.SmartWater
{
    public class Station
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("nomi")]
        public string Name { get; set; }
        [JsonPropertyName("user_id")]
        public int UserId { get; set; }
        [JsonPropertyName("stations")]  
        public string Sensors { get; set; }
    }
}
