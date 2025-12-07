namespace MeasuringDevice
{
    using DeviceControl;
    public class MeasuringMassDevice : MeasureDataDevice, IMeasuringDevice
    {
        //private new const DeviceType measurementType = DeviceType.LENGTH;
        public MeasuringMassDevice(Units units)
        {
            unitsToUse = units;
            measurementType = DeviceType.MASS;
        }

        public override decimal MetricValue()
            => (decimal)( unitsToUse == Units.Metric? mostRecentMeasure : 0.4536 * mostRecentMeasure );

        public override decimal ImperialValue()
            => (decimal)( unitsToUse == Units.Imperial? mostRecentMeasure : 2.2046 * mostRecentMeasure );
    }
}