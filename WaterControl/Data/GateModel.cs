namespace WaterControl.Model
{
    public class GateModel
    {
        public PLCModel Gate = new PLCModel();
        public bool isActive { get; set; } = false;
        public bool isEnebled { get; set; } = true;
        public int encoderID { get; set; }
        public int encoderValue { get; set; }
        public bool  gateBlock { get; set; }

        public GateModel() {
            gateBlock = true;
        }
    }
}
