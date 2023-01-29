
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterControl.Data
{
    public class Licesy
    {
        public int Id { get; set; }
        public string? Password { get; set; }
        public string Today { get; set; }
        public string? MotherboardId { get; set; }
        public string? IsLisecy { get; set; } = "NoLicesy";
        public string GateCount { get; set; }
        public string? IsGeneralLisecy { get; set; } = "NoLicesy";
    }
}
