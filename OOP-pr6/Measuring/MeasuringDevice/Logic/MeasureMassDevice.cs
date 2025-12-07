namespace MeasuringDevice
{
    using DeviceControl;
    public class MeasuringMassDevice : MeasureDataDevice, IMeasuringDevice
    {
        //private new const DeviceType measurementType = DeviceType.LENGTH;
        public MeasuringMassDevice(Units units, int heartBeatInterval)
        {
            unitsToUse = units;
            measurementType = DeviceType.MASS;
            this.HeartBeatInterval = heartBeatInterval;
        }

        public MeasuringMassDevice(Units units, string logFileName="") : this(units, 1000)
        {
            LoggingFileName = logFileName;
        }

        public override decimal MetricValue()
            => (decimal)( unitsToUse == Units.Metric? mostRecentMeasure : 0.4536 * mostRecentMeasure );

        public override decimal ImperialValue()
            => (decimal)( unitsToUse == Units.Imperial? mostRecentMeasure : 2.2046 * mostRecentMeasure );
    }
}