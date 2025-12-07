namespace MeasuringDevice
{
    using DeviceControl;
    using System.ComponentModel;
    using System.Diagnostics.Metrics;

    public abstract class MeasureDataDevice : IEventEnabledMeasuringDevice
    {
        /// <summary>Система измерения</summary>
        protected Units unitsToUse;
        public Units UnitsToUse => unitsToUse;

        /// <summary>Циклический буффер -- история измерений</summary>
        protected int[] dataCaptured = [];
        public int[] DataCaptured => dataCaptured;

        /// <summary>Последнее измерение</summary>
        protected int mostRecentMeasure;
        public int MostRecentMeasure => mostRecentMeasure;

        public string LoggingFileName { get; set; } = null!;

        private StreamWriter loggingFileWriter = null!;

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
            if (LoggingFileName != null && LoggingFileName != "")
                loggingFileWriter = new StreamWriter(LoggingFileName, append: true);

            controller = DeviceController.StartDevice(measurementType);
            GetMeasurements();
            StartHeartBeat();
        }
        
        /// <summary>
        /// Stops the measuring device.
        /// </summary>
        public void StopCollecting()
        {
            dataCollector?.CancelAsync();
            heartBeatTimer?.CancelAsync();

            controller?.StopDevice();

            loggingFileWriter?.Close();
            loggingFileWriter = null!;
        }

        public void Dispose()
        {
            dataCollector?.Dispose();
        }

        public int[] GetRawData() => dataCaptured;

        private BackgroundWorker? dataCollector;
        /// <summary>
        /// Запускает получение данных с эмулируемого устройства
        /// </summary>
        private void GetMeasurements()
        {
            // Создаём объект BackgroundWorker
            dataCollector = new BackgroundWorker();
            // Свойство: поддержка отмены
            dataCollector.WorkerSupportsCancellation = true;
            // Свойство: сообщает о прогрессе во время выполнения
            dataCollector.WorkerReportsProgress = true;

            // Создаём делегаты из методов и подписываем их на события dataCollector
            dataCollector.DoWork += new DoWorkEventHandler(dataCollector_DoWork);
            dataCollector.ProgressChanged += new ProgressChangedEventHandler(dataCollector_ProgressChanged);

            // Запускаем
            dataCollector.RunWorkerAsync();
        }

        // Когда выполнено и сохраннено измерение
        private void dataCollector_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            OnNewMeasurementTaken();
        }

        private void dataCollector_DoWork(object? sender, DoWorkEventArgs e)
        {
            dataCaptured = new int[10];
            int i = 0;
            if (dataCollector == null) return;
            // Пока не завершён сбор данных
            while (!dataCollector.CancellationPending)
            {
                if (controller == null) return;
                dataCaptured[i] = controller.TakeMeasurement();
                mostRecentMeasure = dataCaptured[i];
                // Если устройство выключено 
                if (!controller.IsActive) break;
                // Логируем
                loggingFileWriter?.WriteLine($"Measurement - {mostRecentMeasure}");
                dataCollector.ReportProgress(0); // Вызывает событие ProgressChanged

                i++;
                if (i > 9) i = 0;
            }
        }

        /// <summary>
        /// Событие -- проведено измерение
        /// </summary>
        public event EventHandler? NewMeasurementTaken;

        /// <summary>
        /// Вызов обработчиков
        /// </summary>
        protected virtual void OnNewMeasurementTaken()
        {
            NewMeasurementTaken?.Invoke(this, null!);
        }

        public string GetLoggingFile()
        {
            throw new NotImplementedException();
        }

        protected int heartBeatIntervalTime;
        public int HeartBeatInterval
        { 
            get => heartBeatIntervalTime; 
            protected set => heartBeatIntervalTime = value; 
        }
        public event HeartBeatEventHandler? HeartBeat;
        private BackgroundWorker? heartBeatTimer;
        private void StartHeartBeat()
        {
            if (controller == null) return;

            heartBeatTimer = new BackgroundWorker();
            heartBeatTimer.WorkerSupportsCancellation = true;
            heartBeatTimer.WorkerReportsProgress = true;

            heartBeatTimer.DoWork += (o, args) =>
            {
                while(true){
                    Thread.Sleep(HeartBeatInterval);
                    if (!controller.IsActive) break;

                    heartBeatTimer.ReportProgress(0);
                }
            };

            heartBeatTimer.ProgressChanged += (o, args) =>
            {
                OnHeartBeat();
            };

            heartBeatTimer.RunWorkerAsync();
        }
        protected virtual void OnHeartBeat()
        {
            HeartBeat?.Invoke(this, new HeartBeatEventArgs());
        }
    }
}