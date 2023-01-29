namespace WaterControl.Model
{
    public class encoderModel
    {
        public int value { get; set; }
        public int maxValue { get; set; }
        public int minValu { get; set; }
        public int toothCount { get; set; } = 40;
        public int valuPerCircle { get; set; } = 4096;
        public int toothState { get; set; }

        public int getStateForNextTooth(int tooth)
        {
            toothStateUpdate();
            if (tooth > toothState)
            {
                return value + (tooth - toothState) * (4096 / 40);

            }
            else
            {
                return value - (toothState - tooth) * (4096 / 40);

            }
        }
        public void toothStateUpdate()
        {

            toothState = (value - minValu) / (4096 / 40);
        }
    }
}
