namespace MeasuringDevice
{
    public interface IMeasuringDevice
    {
        /// <summary>
        /// Gets the Units used natively by the device.
        /// </summary>
        Units UnitsToUse { get; }
        /// <summary>
        /// Gets an array of the measurements taken by the device.
        /// </summary>
        int[] DataCaptured { get; }
        /// <summary>
        /// Gets the most recent measurement taken by the device.
        /// </summary>
        int MostRecentMeasure { get; }
        /// <summary>
        /// Gets or sets the name of the logging file used.
        /// If the logging file changes this closes the current file and creates the new file.
        /// </summary>
        string LoggingFileName { get; set; }
        /// <summary>
        /// Конвертирует собираемые данные в метрические единицы
        /// </summary>
        /// <returns>Последнее измерение в метрической системе</returns>
        decimal MetricValue();
        /// <summary>
        /// Конвертирует собираемые данные в имперские единицы
        /// </summary>
        /// <returns>Последнее измерение в имперской системе</returns>
        decimal ImperialValue();
        /// <summary>
        /// Запускает прибор
        /// </summary>
        void StartCollecting();
        /// <summary>
        /// Останавливает прибор
        /// </summary>
        void StopCollecting();
        /// <summary>
        /// Доступ к сырым данным измерений
        /// </summary>
        /// <returns>Сырые данные измерений устройства в нативном формате</returns>
        int[] GetRawData();
        /// <summary>
        /// Returns the file name of the logging file for the device.
        /// </summary>
        /// <returns>The file name of the logging file.</returns>
        string GetLoggingFile();
    }
}