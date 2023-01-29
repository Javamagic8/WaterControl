using System;

namespace WaterControl.Model
{
    public class plcCoreModel
    {
        public bool up { get; set; }
        public bool down { get; set; }
        public bool stop { get; set; }
        public Int64 currentTime { get; set; }
        public Int64 nextTime1Up { get; set; }
        public Int64 nextTime1Down { get; set; }
        public Int64 nextTime1Update { get; set; }     
        public bool stopreport1 { get; set; } = true;
        public bool upreporter1 { get; set; } = true;
        public bool downreporter1 { get; set; } = true;
        public bool updateTime1 { get; set; } = false;
        public int updateTimeInterval { get; set; } = 10;
        public bool timeIntervalChange { get; set; } = false;
        public bool enabled { get; set; } = false;
        public bool statusCheck { get; set; } = true;
        public bool spinActivate { get; set; } = true;
        public bool StopUnlocker { get; set; } = true;

    }
}
