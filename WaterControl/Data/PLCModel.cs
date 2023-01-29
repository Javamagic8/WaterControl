namespace WaterControl.Model
{
    public class PLCModel
    {
        public PLCGateModel GateControl = new PLCGateModel();
        public bool Remote { get; set; } = true;
        public bool Local { get; set; } = false;
        public int Id { get; set; } = 28;
    }
}
