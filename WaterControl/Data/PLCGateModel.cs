namespace WaterControl.Model
{
    public class PLCGateModel
    {
        public int DownOnBit { get; set; }
        public int UpOnBit { get; set; }
        public int UpCheckBit { get; set; }
        public int DownCheckBit { get; set; }
        public int UpOffBit { get; set; }
        public int DownOffBit { get; set; }
        public int UpStop { get; set; }
        public int DownStop { get; set; }
    }
}
