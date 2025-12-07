namespace MeasuringDevice
{
    public interface IMeasuringDevice
    {
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
    }
}