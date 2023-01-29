
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterControl.Data
{
    public class EncoderCoreModel
    {
        public int Id { get; set; }
        public bool Up { get; set; }
        public int EncoderId { get; set; }
        public bool Down { get; set; }
        public bool Stop { get; set; }
        public Int64 CurrentTime { get; set; }
        public Int64 NextTime1Up { get; set; }
        public Int64 NextTime1Down { get; set; }
        public Int64 NextTime1Update { get; set; }
        public bool Stopreport1 { get; set; } = true;
        public bool Upreporter1 { get; set; } = true;
        public bool Downreporter1 { get; set; } = true;
        public bool UpdateTime1 { get; set; } = false;
        public int UpdateTimeInterval { get; set; } = 10;
        public bool TimeIntervalChange { get; set; } = false;
        public bool Enabled { get; set; } = false;
        public bool StatusCheck { get; set; } = true;
        public bool SpinActivate { get; set; } = true;
        public bool StopUnlocker { get; set; } = true;
    }
}
