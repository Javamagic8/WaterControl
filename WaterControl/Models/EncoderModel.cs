

namespace WaterControl.Data
{
    public class EncoderModel
    {
        public int Id { get; set; }
        public int Value { get; set; } = 0;
        public int MaxValue { get; set; }
        public int MinValu { get; set; }
        public int ToothCount { get; set; } = 40;
        public int ValuPerCircle { get; set; } = 4096;
        public int ToothState { get; set; } = 0;
        public int absolutValueEncoder { get; set; }
        public bool IsReverseValue { get; set; } = false;
        public int getStateForNextTooth(int Tooth)
        {
            toothStateUpdate();
            if (Tooth > ToothState)
            {
                return Value + (Tooth - ToothState) * (4096 / ToothCount);

            }
            else
            {
                return Value - (ToothState - Tooth) * (4096 / ToothCount);

            }
        }
        public void toothStateUpdate()
        {

            ToothState = (Value - MinValu) / (4096 / ToothCount);
        }
    }
}
