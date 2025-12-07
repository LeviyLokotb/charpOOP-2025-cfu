namespace MeasuringDevice
{
    using DeviceControl;
    public class MeasuringLengthDevice : MeasureDataDevice, IMeasuringDevice
    {
        //private new const DeviceType measurementType = DeviceType.LENGTH;
        public MeasuringLengthDevice(Units units)
        {
            unitsToUse = units;
            measurementType = DeviceType.LENGTH;
        }

        public override decimal MetricValue()
            => (decimal)( unitsToUse == Units.Metric? mostRecentMeasure : 25.4 * mostRecentMeasure );

        public override decimal ImperialValue()
            => (decimal)( unitsToUse == Units.Imperial? mostRecentMeasure : 0.03937 * mostRecentMeasure );
    }
}