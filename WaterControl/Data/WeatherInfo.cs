using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WaterControl.Data
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Condition
    {
        public string text { get; set; }
    }

    public class Current
    {
        public double temp_c { get; set; }
        public Condition condition { get; set; }
        public double wind_kph { get; set; }
        public int cloud { get; set; }

    }
    public class Location
    {
        public string region { get; set; }
    }

    public class WeatherInfo
    {
        public Location location { get; set; }
        public Current current { get; set; }
    }

}
