using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace WaterControl.Data.SmartWater
{
    public class TokenInfo
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
        [JsonPropertyName("expired at")]
        public string ExpiredAt { get; set; }
        [JsonPropertyName("stations")]
        public List<Station> Stations { get; set; }
        [JsonPropertyName("role")]
        public string RolsString { get; set; }
        [JsonPropertyName("fio")]
        public string FIO { get; set; }
    }

    public class TokenDb
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string ExpiredAt { get; set; }
        public string RolsString { get; set; }
        public string FIO { get; set; }
        public int StationID { get; set; }
    }
}
