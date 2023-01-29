namespace WaterControl.Model
{
    public class gateActionManipulator
    {
        public bool isActive { get; set; } = false;
        public bool active2Up { get; set; } = false;
        public bool active2Down { get; set; } = false;
        public bool deactivate2Stop { get; set; } = true;
    }
}
