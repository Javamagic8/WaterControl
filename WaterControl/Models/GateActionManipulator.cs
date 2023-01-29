
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterControl.Data
{
    public class GateManipuller
    {
        public int Id { get; set; }
        public bool IsActive { get; set; } = false;
        public bool Active2Up { get; set; } = false;
        public bool Active2Down { get; set; } = false;
        public bool Deactivate2Stop { get; set; } = true;
    }

    public class GateManipullerEncoder
    {
        public int Id { get; set; }
        public bool IsActive { get; set; } = false;
        public bool Active2Up { get; set; } = false;
        public bool Active2Down { get; set; } = false;
        public bool Deactivate2Stop { get; set; } = true;
    }
}
