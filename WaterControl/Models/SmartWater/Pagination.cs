using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WaterControl.Data.SmartWater
{
    public class Pagination
    {
        [JsonPropertyName("totalCount")]
        public int TotalCount { get; set; }
        [JsonPropertyName("defaultPerPage")]
        public int DefaultPerPage { get; set; }
        [JsonPropertyName("perPage")]
        public int PerPage { get; set; }
        [JsonPropertyName("pageCount")]
        public int PageCount { get; set; }
        [JsonPropertyName("currentPage")]
        public int CurrentPage { get; set; }
    }
}
