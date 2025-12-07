namespace MeasuringDevice
{
    using DeviceControl;
    public abstract class MeasureDataDevice : IMeasuringDevice
    {
        /// <summary>Система измерения</summary>
        protected Units unitsToUse;
        /// <summary>Циклический буффер -- история измерений</summary>
        protected int[] dataCaptured = [];
        /// <summary>Последнее измерение</summary>
        protected int mostRecentMeasure;
        /// <summary>Эмулируемое устройство</summary>
        protected IDeviceController? controller = null;
        /// <summary>Тип эмулируемого устройства</summary>
        protected DeviceType measurementType;
        /// <summary>
        /// Converts the raw data collected by the measuring device into a metric value.
        /// </summary>
        /// <returns>The latest measurement from the device converted to metric units.</returns>
        public abstract decimal MetricValue();
        
        /// <summary>
        /// Converts the raw data collected by the measuring device into an imperial value.
        /// </summary>
        /// <returns>The latest measurement from the device converted to imperial units.</returns>
        public abstract decimal ImperialValue();

        /// <summary>
        /// Последнее измерение в нативной системе измерения
        /// </summary>
        /// <returns></returns>
        public decimal NativeValue()
            => (decimal)mostRecentMeasure;
        
        /// <summary>
        /// Starts the measuring device.
        /// </summary>
        public void StartCollecting()
        {
            controller = DeviceController.StartDevice(measurementType);
            GetMeasurements();
        }
        
        /// <summary>
        /// Stops the measuring device.
        /// </summary>
        public void StopCollecting()
        {
            if (controller != null)
            {
                controller.StopDevice();
                controller = null;
            }
        }
        public int[] GetRawData() => dataCaptured;

        /// <summary>
        /// Запускает получение данных с эмулируемого устройства
        /// </summary>
        private void GetMeasurements()
        {
            this.dataCaptured = new int[10];
            System.Threading.ThreadPool.QueueUserWorkItem((dummy) =>
            {
                int x = 0;
                Random timer = new Random();
                while (controller != null)
                {
                    System.Threading.Thread.Sleep(timer.Next(1000, 5000));
                    dataCaptured[x] = controller != null ?
                        controller.TakeMeasurement() : dataCaptured[x];
                    mostRecentMeasure = dataCaptured[x];
                    x++;
                    if (x == 10) x = 0;
                }
            });
        }
    }
}